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

    public async Task<ApiResponse<DailySalesReportDto>> GetDailySalesReportAsync(DateOnly date, Guid? branchId)
    {
        if (!branchId.HasValue) return ApiResponse<DailySalesReportDto>.Fail("Şube belirtilmelidir.");

        var branch = await _context.Branches.FindAsync(branchId.Value);
        if (branch == null) return ApiResponse<DailySalesReportDto>.Fail("Şube bulunamadı.");

        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .ThenInclude(d => d.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.BranchId == branchId.Value && c.Date == date);

        if (closing == null) return ApiResponse<DailySalesReportDto>.Fail("Bu tarihe ait gün kapanış verisi bulunamadı.");

        var demandItems = await _context.DemandItems
            .Include(i => i.Demand)
            .Where(i => i.Demand.SalesBranchId == branchId.Value && DateOnly.FromDateTime(i.Demand.CreatedAt.Date) == date && i.Status == DemandItemStatus.Received)
            .ToListAsync();

        var wastes = await _context.Wastes
            .Where(w => w.BranchId == branchId.Value && w.Date == date)
            .ToListAsync();

        var report = new DailySalesReportDto
        {
            Date = date.ToDateTime(TimeOnly.MinValue),
            BranchId = branchId.Value,
            BranchName = branch.Name
        };

        foreach (var detail in closing.Details)
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
            report.Items.Add(item);
        }

        report.TotalCalculatedSales = report.Items.Sum(i => i.CalculatedSales);
        report.TotalWaste = report.Items.Sum(i => i.WasteQuantity);
        report.TotalSalesValue = report.Items.Sum(i => i.SalesValue);

        return ApiResponse<DailySalesReportDto>.Ok(report);
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
}
