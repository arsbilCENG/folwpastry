using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Reports;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class ReportService : IReportService
{
    private readonly IPastryFlowDbContext _context;

    public ReportService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<DailyReportDto>> GetDailySalesReportAsync(DateOnly date, Guid? branchId)
    {
        if (!branchId.HasValue) return ApiResponse<DailyReportDto>.Fail("Şube belirtilmelidir.");

        var branch = await _context.Branches.FindAsync(branchId.Value);
        if (branch == null) return ApiResponse<DailyReportDto>.Fail("Şube bulunamadı.");

        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .ThenInclude(d => d.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.BranchId == branchId.Value && c.Date == date);

        if (closing == null) return ApiResponse<DailyReportDto>.Fail("Bu tarihe ait gün kapanış verisi bulunamadı.");

        var demandItems = await _context.DemandItems
            .Include(i => i.Demand)
            .Where(i => i.Demand.SalesBranchId == branchId.Value && DateOnly.FromDateTime(i.Demand.CreatedAt.Date) == date && i.Status == DemandItemStatus.Received)
            .ToListAsync();

        var wastes = await _context.Wastes
            .Where(w => w.BranchId == branchId.Value && w.Date == date)
            .ToListAsync();

        var dto = new DailyReportDto
        {
            Date = date.ToDateTime(TimeOnly.MinValue),
            BranchId = branchId.Value,
            BranchName = branch.Name
        };

        foreach (var detail in closing.Details.Where(d => d.Product.TrackingType != TrackingType.Counter))
        {
            var item = new DailySalesItemDto
            {
                ProductId = detail.ProductId,
                ProductName = detail.Product.Name,
                CategoryName = detail.Product.Category.Name,
                UnitType = detail.Product.Unit.ToString(),
                OpeningStock = detail.OpeningStock,
                ReceivedFromDemand = demandItems.Where(i => i.ProductId == detail.ProductId).Sum(i => i.ApprovedQuantity ?? 0),
                WasteQuantity = wastes.Where(w => w.ProductId == detail.ProductId).Sum(w => w.Quantity),
                EndOfDayCount = detail.EndOfDayCount,
                CalculatedSales = detail.CalculatedSales,
                UnitPrice = detail.Product.UnitPrice,
                SalesValue = detail.Product.UnitPrice.HasValue ? detail.CalculatedSales * detail.Product.UnitPrice : null
            };
            dto.Items.Add(item);
        }

        dto.TotalCalculatedSales = dto.Items.Sum(i => i.CalculatedSales);
        dto.TotalWaste = dto.Items.Sum(i => i.WasteQuantity);
        dto.TotalSalesValue = dto.Items.Sum(i => i.SalesValue);

        // Counter satış geliri — DayClosingDetail üzerinden
        var counterSalesRevenue = await _context.DayClosingDetails
            .Where(d => d.DayClosing.BranchId == branchId
                     && d.DayClosing.Date == date
                     && d.Product.TrackingType == TrackingType.Counter
                     && d.CounterSoldQuantity.HasValue)
            .SumAsync(d => d.CounterSoldQuantity!.Value * (d.Product.UnitPrice ?? 0));

        // Satın alım giderleri
        var purchases = await _context.Purchases
            .Where(p => p.BranchId == branchId
                     && DateOnly.FromDateTime(p.PurchaseDate) == date
                     && !p.IsDeleted)
            .ToListAsync();

        var totalPurchaseExpense = purchases.Sum(p => p.TotalAmount);
        var cashPurchaseExpense = purchases
            .Where(p => p.PaymentMethod == PaymentMethod.Cash)
            .Sum(p => p.TotalAmount);
        var cardPurchaseExpense = purchases
            .Where(p => p.PaymentMethod == PaymentMethod.CreditCard)
            .Sum(p => p.TotalAmount);

        // Kasa hareketleri
        var cashTransactions = await _context.WalletTransactions
            .Where(t => (t.SourceBranchId == branchId || t.TargetBranchId == branchId)
                     && DateOnly.FromDateTime(t.TransactionDate) == date
                     && t.WalletType == WalletType.Cash)
            .ToListAsync();

        var totalDeposits = cashTransactions
            .Where(t => t.TransactionType == WalletTransactionType.AdminToBranch)
            .Sum(t => t.Amount);
        var totalWithdrawals = cashTransactions
            .Where(t => t.TransactionType == WalletTransactionType.BranchToAdmin)
            .Sum(t => t.Amount);

        // DTO'ya ekle
        dto.CounterSalesRevenue = counterSalesRevenue;
        dto.TotalPurchaseExpense = totalPurchaseExpense;
        dto.CashPurchaseExpense = cashPurchaseExpense;
        dto.CardPurchaseExpense = cardPurchaseExpense;
        dto.TotalCashDeposits = totalDeposits;
        dto.TotalCashWithdrawals = totalWithdrawals;

        return ApiResponse<DailyReportDto>.Ok(dto);
    }

    public async Task<ApiResponse<WasteSummaryReportDto>> GetWasteSummaryReportAsync(DateOnly startDate, DateOnly endDate, Guid? branchId, Guid? categoryId)
    {
        var query = _context.Wastes
            .Include(w => w.Product)
            .ThenInclude(p => p.Category)
            .Where(w => w.Date >= startDate && w.Date <= endDate)
            .AsQueryable();

        if (branchId.HasValue) query = query.Where(w => w.BranchId == branchId.Value);
        if (categoryId.HasValue) query = query.Where(w => w.Product.CategoryId == categoryId.Value);

        var wastes = await query.ToListAsync();

        var report = new WasteSummaryReportDto
        {
            StartDate = startDate.ToDateTime(TimeOnly.MinValue),
            EndDate = endDate.ToDateTime(TimeOnly.MinValue),
            BranchId = branchId
        };

        if (branchId.HasValue) 
        {
            var branch = await _context.Branches.FindAsync(branchId.Value);
            report.BranchName = branch?.Name;
        }

        var grouped = wastes
            .GroupBy(w => w.ProductId)
            .Select(g => new WasteSummaryItemDto
            {
                ProductId = g.Key,
                ProductName = g.First().Product.Name,
                CategoryName = g.First().Product.Category.Name,
                UnitType = g.First().Product.Unit.ToString(),
                TotalQuantity = g.Sum(w => w.Quantity),
                WasteCount = g.Count(),
                EstimatedLoss = g.First().Product.UnitPrice.HasValue ? g.Sum(w => w.Quantity) * g.First().Product.UnitPrice : null
            }).ToList();

        report.Items = grouped;
        report.TotalWasteQuantity = grouped.Sum(i => i.TotalQuantity);
        report.TotalEstimatedLoss = grouped.Sum(i => i.EstimatedLoss);

        return ApiResponse<WasteSummaryReportDto>.Ok(report);
    }

    public async Task<ApiResponse<DemandSummaryReportDto>> GetDemandSummaryReportAsync(DateOnly startDate, DateOnly endDate, Guid? branchId)
    {
        var query = _context.Demands
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .Include(d => d.Items)
            .Where(d => DateOnly.FromDateTime(d.CreatedAt.Date) >= startDate && DateOnly.FromDateTime(d.CreatedAt.Date) <= endDate)
            .AsQueryable();

        if (branchId.HasValue) query = query.Where(d => d.SalesBranchId == branchId.Value || d.ProductionBranchId == branchId.Value);

        var demands = await query.ToListAsync();

        var report = new DemandSummaryReportDto
        {
            StartDate = startDate.ToDateTime(TimeOnly.MinValue),
            EndDate = endDate.ToDateTime(TimeOnly.MinValue),
            TotalDemands = demands.Count,
            TotalApproved = demands.Count(d => d.Status == DemandStatus.Approved || d.Status == DemandStatus.PartiallyApproved),
            TotalRejected = demands.Count(d => d.Status == DemandStatus.Rejected),
            Items = demands.Select(d => new DemandSummaryItemDto
            {
                Date = d.CreatedAt,
                FromBranchId = d.SalesBranchId,
                FromBranchName = d.SalesBranch.Name,
                ToBranchId = d.ProductionBranchId,
                ToBranchName = d.ProductionBranch.Name,
                TotalItems = d.Items.Count,
                ApprovedItems = d.Items.Count(i => i.Status == DemandItemStatus.Approved),
                RejectedItems = d.Items.Count(i => i.Status == DemandItemStatus.Rejected),
                Status = d.Status.ToString()
            }).ToList()
        };

        if (report.TotalDemands > 0)
            report.ApprovalRate = (decimal)report.TotalApproved / report.TotalDemands * 100;

        return ApiResponse<DemandSummaryReportDto>.Ok(report);
    }

    public async Task<ApiResponse<BranchComparisonReportDto>> GetBranchComparisonReportAsync(DateOnly startDate, DateOnly endDate, string metric)
    {
        var branches = await _context.Branches.Where(b => b.BranchType == BranchType.Sales).ToListAsync();
        var report = new BranchComparisonReportDto
        {
            StartDate = startDate.ToDateTime(TimeOnly.MinValue),
            EndDate = endDate.ToDateTime(TimeOnly.MinValue),
            Metric = metric
        };

        foreach (var branch in branches)
        {
            var item = new BranchComparisonItemDto
            {
                BranchId = branch.Id,
                BranchName = branch.Name
            };

            if (metric == "sales")
            {
                var salesDetails = await _context.DayClosingDetails
                    .Include(d => d.DayClosing)
                    .Where(d => d.DayClosing.BranchId == branch.Id && d.DayClosing.Date >= startDate && d.DayClosing.Date <= endDate)
                    .ToListAsync();

                item.DailyData = salesDetails
                    .GroupBy(d => d.DayClosing.Date)
                    .Select(g => new DailyMetricDto
                    {
                        Date = g.Key.ToDateTime(TimeOnly.MinValue),
                        Value = g.Sum(d => d.CalculatedSales)
                    }).OrderBy(g => g.Date).ToList();
            }
            else if (metric == "waste")
            {
                var wastes = await _context.Wastes
                    .Where(w => w.BranchId == branch.Id && w.Date >= startDate && w.Date <= endDate)
                    .ToListAsync();

                item.DailyData = wastes
                    .GroupBy(w => w.Date)
                    .Select(g => new DailyMetricDto
                    {
                        Date = g.Key.ToDateTime(TimeOnly.MinValue),
                        Value = g.Sum(w => w.Quantity)
                    }).OrderBy(g => g.Date).ToList();
            }
            else if (metric == "demand")
            {
                var demands = await _context.Demands
                    .Where(d => d.SalesBranchId == branch.Id && DateOnly.FromDateTime(d.CreatedAt.Date) >= startDate && DateOnly.FromDateTime(d.CreatedAt.Date) <= endDate)
                    .ToListAsync();

                item.DailyData = demands
                    .GroupBy(d => DateOnly.FromDateTime(d.CreatedAt.Date))
                    .Select(g => new DailyMetricDto
                    {
                        Date = g.Key.ToDateTime(TimeOnly.MinValue),
                        Value = g.Count()
                    }).OrderBy(g => g.Date).ToList();
            }

            item.Total = item.DailyData.Sum(d => d.Value);
            report.Items.Add(item);
        }

        return ApiResponse<BranchComparisonReportDto>.Ok(report);
    }

    public async Task<PurchaseReportDto> GetPurchaseReportAsync(
        Guid? branchId, DateTime startDate, DateTime endDate)
    {
        var query = _context.Purchases
            .Include(p => p.Branch)
            .Include(p => p.Items)
            .Where(p => p.PurchaseDate.Date >= startDate.Date
                     && p.PurchaseDate.Date <= endDate.Date
                     && !p.IsDeleted);

        if (branchId.HasValue)
            query = query.Where(p => p.BranchId == branchId.Value);

        var purchases = await query
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();

        return new PurchaseReportDto
        {
            StartDate = startDate,
            EndDate = endDate,
            BranchName = branchId.HasValue
                ? purchases.FirstOrDefault()?.Branch?.Name
                : null,
            TotalExpense = purchases.Sum(p => p.TotalAmount),
            CashExpense = purchases
                .Where(p => p.PaymentMethod == PaymentMethod.Cash)
                .Sum(p => p.TotalAmount),
            CardExpense = purchases
                .Where(p => p.PaymentMethod == PaymentMethod.CreditCard)
                .Sum(p => p.TotalAmount),
            Purchases = purchases.Select(p => new PurchaseReportItemDto
            {
                PurchaseId = p.Id,
                PurchaseNumber = p.PurchaseNumber,
                PurchaseDate = p.PurchaseDate,
                BranchName = p.Branch?.Name ?? string.Empty,
                PaymentMethodLabel = p.PaymentMethod == PaymentMethod.Cash
                    ? "Nakit" : "Kredi Kartı",
                TotalAmount = p.TotalAmount,
                Notes = p.Notes,
                Items = p.Items.Select(i => new PurchaseReportItemDetailDto
                {
                    ItemName = i.ItemName,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            }).ToList()
        };
    }

    public async Task<CashTransactionReportDto> GetCashTransactionReportAsync(
        Guid? branchId, DateTime startDate, DateTime endDate)
    {
        var query = _context.WalletTransactions
            .Include(t => t.SourceBranch)
            .Include(t => t.TargetBranch)
            .Include(t => t.CreatedBy)
            .Where(t => t.TransactionDate.Date >= startDate.Date
                     && t.TransactionDate.Date <= endDate.Date
                     && (t.TransactionType == WalletTransactionType.AdminToBranch || t.TransactionType == WalletTransactionType.BranchToAdmin)
                     && t.WalletType == WalletType.Cash);

        if (branchId.HasValue)
            query = query.Where(t => t.SourceBranchId == branchId.Value || t.TargetBranchId == branchId.Value);

        var transactions = await query
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

        var totalDeposits = transactions
            .Where(t => t.TransactionType == WalletTransactionType.AdminToBranch)
            .Sum(t => t.Amount);
        var totalWithdrawals = transactions
            .Where(t => t.TransactionType == WalletTransactionType.BranchToAdmin)
            .Sum(t => t.Amount);

        return new CashTransactionReportDto
        {
            StartDate = startDate,
            EndDate = endDate,
            BranchName = branchId.HasValue
                ? transactions.FirstOrDefault(t => t.TargetBranch != null)?.TargetBranch?.Name ?? transactions.FirstOrDefault(t => t.SourceBranch != null)?.SourceBranch?.Name
                : null,
            TotalDeposits = totalDeposits,
            TotalWithdrawals = totalWithdrawals,
            NetFlow = totalDeposits - totalWithdrawals,
            Transactions = transactions.Select(t => new CashTransactionReportItemDto
            {
                TransactionId = t.Id,
                TransactionDate = t.TransactionDate,
                BranchName = t.TargetBranch?.Name ?? t.SourceBranch?.Name ?? string.Empty,
                TransactionTypeLabel = t.TransactionType == WalletTransactionType.AdminToBranch
                    ? "Para Yatırma" : "Para Çekme",
                MethodLabel = "Nakit",
                Amount = t.Amount,
                Description = t.Description,
                CreatedByName = t.CreatedBy?.Email ?? string.Empty
            }).ToList()
        };
    }
}
