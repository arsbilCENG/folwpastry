using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.DTOs.Report;
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

    /// <summary>
    /// Npgsql: <c>timestamp with time zone</c> parametreleri için <see cref="DateTimeKind.Utc"/> gerekir.
    /// <see cref="DateOnly.ToDateTime(TimeOnly)"/> <see cref="DateTimeKind.Unspecified"/> üretir ve sorgu patlar.
    /// </summary>
    private static DateTime UtcStartOfDay(DateOnly date) =>
        DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);

    /// <summary>Gün sonu (hariç): bir sonraki günün 00:00 UTC.</summary>
    private static DateTime UtcStartOfNextDay(DateOnly date) =>
        UtcStartOfDay(date.AddDays(1));

    public async Task<DailySummaryReportDto> GetDailySummaryAsync(
        Guid branchId, DateOnly date)
    {
        var branch = await _context.Branches.FindAsync(branchId);

        var closing = await _context.DayClosings
            .Include(c => c.Details)
                .ThenInclude(d => d.Product)
                    .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.BranchId == branchId
                                   && c.Date == date
                                   && c.IsClosed);

        if (closing == null)
        {
            return new DailySummaryReportDto
            {
                Date = date,
                BranchName = branch?.Name ?? string.Empty,
                IsClosed = false
            };
        }

        var productSales = closing.Details
            .Where(d => d.Product != null && d.Product.IsActive
                     && d.Product.TrackingType != TrackingType.Counter
                     && d.CalculatedSales > 0)
            .Select(d => new DailyProductSaleDto
            {
                CategoryName = d.Product.Category?.Name ?? string.Empty,
                ProductName = d.Product.Name,
                Unit = d.Product.Unit.ToString(),
                SoldQuantity = d.CalculatedSales,
                UnitPrice = d.Product.UnitPrice,
                Revenue = d.CalculatedSales * (d.Product.UnitPrice ?? 0),
                IsCounter = false
            })
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.ProductName)
            .ToList();

        var counterSales = closing.Details
            .Where(d => d.Product != null
                     && d.Product.TrackingType == TrackingType.Counter
                     && (d.CounterSoldQuantity ?? 0) > 0)
            .Select(d => new DailyProductSaleDto
            {
                CategoryName = d.Product.Category?.Name ?? string.Empty,
                ProductName = d.Product.Name,
                Unit = d.Product.Unit.ToString(),
                SoldQuantity = d.CounterSoldQuantity ?? 0,
                UnitPrice = d.Product.UnitPrice,
                Revenue = (d.CounterSoldQuantity ?? 0) * (d.Product.UnitPrice ?? 0),
                IsCounter = true
            })
            .OrderBy(x => x.ProductName)
            .ToList();

        var productSalesRevenue = productSales.Sum(x => x.Revenue);
        var counterSalesRevenue = counterSales.Sum(x => x.Revenue);

        var dayStartUtc = UtcStartOfDay(date);
        var nextDayStartUtc = UtcStartOfNextDay(date);

        var purchases = await _context.Purchases
            .Where(p => p.BranchId == branchId
                     && p.PurchaseDate >= dayStartUtc
                     && p.PurchaseDate < nextDayStartUtc
                     && !p.IsDeleted)
            .ToListAsync();

        var wastes = await _context.Wastes
            .Include(w => w.Product)
                .ThenInclude(p => p.Category)
            .Where(w => w.BranchId == branchId && w.Date == date)
            .ToListAsync();

        return new DailySummaryReportDto
        {
            Date = date,
            BranchName = branch?.Name ?? string.Empty,
            IsClosed = true,
            ProductSalesRevenue = productSalesRevenue,
            CounterSalesRevenue = counterSalesRevenue,
            TotalSalesRevenue = productSalesRevenue + counterSalesRevenue,
            TotalPurchaseExpense = purchases.Sum(p => p.TotalAmount),
            CashPurchaseExpense = purchases
                .Where(p => p.PaymentMethod == PaymentMethod.Cash)
                .Sum(p => p.TotalAmount),
            CardPurchaseExpense = purchases
                .Where(p => p.PaymentMethod == PaymentMethod.CreditCard)
                .Sum(p => p.TotalAmount),
            ExpectedCashAmount = closing.ExpectedCashAmount ?? 0,
            ActualCashAmount = closing.CashAmount ?? 0,
            PosAmount = closing.PosAmount ?? 0,
            CashDifference = closing.CashDifference ?? 0,
            WasteItemCount = wastes.Count,
            TotalWasteQuantity = wastes.Sum(w => w.Quantity),
            ProductSales = productSales.Concat(counterSales).ToList(),
            Wastes = wastes.Select(w => new DailyWasteDto
            {
                ProductName = w.Product?.Name ?? string.Empty,
                CategoryName = w.Product?.Category?.Name ?? string.Empty,
                Quantity = w.Quantity,
                Unit = w.Product?.Unit.ToString() ?? string.Empty,
                Reason = w.Notes,
                WasteTypeLabel = w.WasteType == WasteType.EndOfDay
                    ? "Gün Sonu" : "Gün İçi"
            }).ToList()
        };
    }

    public async Task<PeriodSummaryReportDto> GetPeriodSummaryAsync(
        Guid branchId, DateOnly startDate, DateOnly endDate)
    {
        var branch = await _context.Branches.FindAsync(branchId);

        var closings = await _context.DayClosings
            .Include(c => c.Details)
                .ThenInclude(d => d.Product)
                    .ThenInclude(p => p.Category)
            .Where(c => c.BranchId == branchId
                     && c.Date >= startDate
                     && c.Date <= endDate
                     && c.IsClosed)
            .OrderBy(c => c.Date)
            .ToListAsync();

        var periodStartUtc = UtcStartOfDay(startDate);
        var periodEndExclusiveUtc = UtcStartOfNextDay(endDate);

        var purchases = await _context.Purchases
            .Where(p => p.BranchId == branchId
                     && p.PurchaseDate >= periodStartUtc
                     && p.PurchaseDate < periodEndExclusiveUtc
                     && !p.IsDeleted)
            .ToListAsync();

        var dailyRows = closings.Select(c =>
        {
            var productRev = c.Details
                .Where(d => d.Product?.TrackingType != TrackingType.Counter)
                .Sum(d => d.CalculatedSales * (d.Product?.UnitPrice ?? 0));

            var counterRev = c.Details
                .Where(d => d.Product?.TrackingType == TrackingType.Counter)
                .Sum(d => (d.CounterSoldQuantity ?? 0) * (d.Product?.UnitPrice ?? 0));

            var dayStartUtc = UtcStartOfDay(c.Date);
            var nextDayUtc = UtcStartOfNextDay(c.Date);
            var dayPurchase = purchases
                .Where(p => p.PurchaseDate >= dayStartUtc && p.PurchaseDate < nextDayUtc)
                .Sum(p => p.TotalAmount);

            return new PeriodDailyRowDto
            {
                Date = c.Date,
                ProductSalesRevenue = productRev,
                CounterSalesRevenue = counterRev,
                TotalSalesRevenue = productRev + counterRev,
                PurchaseExpense = dayPurchase,
                CashDifference = c.CashDifference ?? 0
            };
        }).ToList();

        var productGroups = closings
            .SelectMany(c => c.Details.Where(d => d.Product != null && d.Product.IsActive))
            .GroupBy(d => d.ProductId)
            .Select(g =>
            {
                var product = g.First().Product;
                var isCounter = product.TrackingType == TrackingType.Counter;
                var totalQty = isCounter
                    ? g.Sum(d => d.CounterSoldQuantity ?? 0)
                    : g.Sum(d => d.CalculatedSales);

                return new PeriodProductSummaryDto
                {
                    CategoryName = product.Category?.Name ?? string.Empty,
                    ProductName = product.Name,
                    Unit = product.Unit.ToString(),
                    TotalSoldQuantity = totalQty,
                    TotalRevenue = totalQty * (product.UnitPrice ?? 0),
                    IsCounter = isCounter
                };
            })
            .Where(x => x.TotalSoldQuantity > 0)
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.ProductName)
            .ToList();

        var wastes = await _context.Wastes
            .Where(w => w.BranchId == branchId
                     && w.Date >= startDate
                     && w.Date <= endDate)
            .ToListAsync();

        return new PeriodSummaryReportDto
        {
            StartDate = startDate,
            EndDate = endDate,
            BranchName = branch?.Name ?? string.Empty,
            ClosedDayCount = closings.Count,
            TotalSalesRevenue = dailyRows.Sum(r => r.TotalSalesRevenue),
            TotalCounterRevenue = dailyRows.Sum(r => r.CounterSalesRevenue),
            TotalPurchaseExpense = purchases.Sum(p => p.TotalAmount),
            TotalCashDifference = dailyRows.Sum(r => r.CashDifference),
            TotalWasteQuantity = wastes.Sum(w => w.Quantity),
            DailyRows = dailyRows,
            ProductSummaries = productGroups
        };
    }

    public async Task<ManagementReportDto> GetManagementReportAsync(
        DateOnly startDate, DateOnly endDate)
    {
        var branches = await _context.Branches
            .Where(b => b.IsActive && !b.IsDeleted)
            .OrderBy(b => b.Name)
            .ToListAsync();

        var periodStartUtc = UtcStartOfDay(startDate);
        var periodEndExclusiveUtc = UtcStartOfNextDay(endDate);

        var branchComparisons = new List<BranchComparisonDto>();

        foreach (var branch in branches)
        {
            var closings = await _context.DayClosings
                .Include(c => c.Details)
                    .ThenInclude(d => d.Product)
                .Where(c => c.BranchId == branch.Id
                         && c.Date >= startDate
                         && c.Date <= endDate
                         && c.IsClosed)
                .ToListAsync();

            var purchases = await _context.Purchases
                .Where(p => p.BranchId == branch.Id
                         && p.PurchaseDate >= periodStartUtc
                         && p.PurchaseDate < periodEndExclusiveUtc
                         && !p.IsDeleted)
                .ToListAsync();

            var totalRevenue = closings.SelectMany(c => c.Details).Sum(d =>
                d.Product?.TrackingType == TrackingType.Counter
                    ? (d.CounterSoldQuantity ?? 0) * (d.Product?.UnitPrice ?? 0)
                    : d.CalculatedSales * (d.Product?.UnitPrice ?? 0));

            var totalExpense = purchases.Sum(p => p.TotalAmount);
            var totalCashDiff = closings.Sum(c => c.CashDifference ?? 0);

            branchComparisons.Add(new BranchComparisonDto
            {
                BranchName = branch.Name,
                TotalSalesRevenue = totalRevenue,
                TotalPurchaseExpense = totalExpense,
                TotalCashDifference = totalCashDiff,
                NetRevenue = totalRevenue - totalExpense,
                ClosedDayCount = closings.Count
            });
        }

        var walletBalances = new List<BranchWalletSummaryDto>();

        foreach (var branch in branches)
        {
            var cashWallet = await _context.BranchWallets
                .FirstOrDefaultAsync(w => w.BranchId == branch.Id
                                       && w.WalletType == WalletType.Cash);
            var bankWallet = await _context.BranchWallets
                .FirstOrDefaultAsync(w => w.BranchId == branch.Id
                                       && w.WalletType == WalletType.Bank);

            walletBalances.Add(new BranchWalletSummaryDto
            {
                BranchName = branch.Name,
                CashBalance = cashWallet?.CurrentBalance ?? 0,
                BankBalance = bankWallet?.CurrentBalance ?? 0,
                TotalBalance = (cashWallet?.CurrentBalance ?? 0)
                             + (bankWallet?.CurrentBalance ?? 0)
            });
        }

        var walletMovements = await _context.WalletTransactions
            .Include(t => t.SourceBranch)
            .Include(t => t.TargetBranch)
            .Include(t => t.CreatedBy)
            .Where(t => t.TransactionDate >= periodStartUtc
                     && t.TransactionDate < periodEndExclusiveUtc
                     && (t.TransactionType == WalletTransactionType.BranchToAdmin
                      || t.TransactionType == WalletTransactionType.AdminToBranch
                      || t.TransactionType == WalletTransactionType.ManualAdjustment))
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => new WalletMovementDto
            {
                TransactionDate = t.TransactionDate,
                BranchName = t.SourceBranch != null
                    ? t.SourceBranch.Name
                    : t.TargetBranch != null
                        ? t.TargetBranch.Name
                        : "Admin",
                TransactionTypeLabel = t.TransactionType == WalletTransactionType.BranchToAdmin
                    ? "Şubeden Çekim"
                    : t.TransactionType == WalletTransactionType.AdminToBranch
                        ? "Şubeye Gönderim"
                        : "Manuel Düzeltme",
                WalletTypeLabel = t.WalletType == WalletType.Cash ? "Nakit" : "Banka",
                Amount = t.Amount,
                Description = t.Description,
                CreatedByName = t.CreatedBy != null ? t.CreatedBy.Email : string.Empty
            })
            .ToListAsync();

        return new ManagementReportDto
        {
            StartDate = startDate,
            EndDate = endDate,
            BranchComparisons = branchComparisons,
            WalletBalances = walletBalances,
            WalletMovements = walletMovements,
            GrandTotalRevenue = branchComparisons.Sum(b => b.TotalSalesRevenue),
            GrandTotalExpense = branchComparisons.Sum(b => b.TotalPurchaseExpense),
            GrandTotalCashBalance = walletBalances.Sum(b => b.CashBalance),
            GrandTotalBankBalance = walletBalances.Sum(b => b.BankBalance)
        };
    }
}
