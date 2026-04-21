using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IPastryFlowDbContext _context;

    public AdminDashboardService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<AdminDashboardDto>> GetDashboardSummaryAsync(DateTime? date = null)
    {
        // Default to today in Turkey time (UTC+3)
        var targetDate = date ?? TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
        var dateOnly = DateOnly.FromDateTime(targetDate);

        var branches = await _context.Branches.Where(b => !b.IsDeleted).ToListAsync();
        
        var demands = await _context.Demands
            .Where(d => !d.IsDeleted && DateOnly.FromDateTime(d.CreatedAt.Date) == dateOnly)
            .ToListAsync();

        var wastes = await _context.Wastes
            .Where(w => !w.IsDeleted && w.Date == dateOnly)
            .ToListAsync();

        var closings = await _context.DayClosings
            .Include(c => c.Details)
            .Where(c => !c.IsDeleted && c.Date == dateOnly)
            .ToListAsync();

        var dashboard = new AdminDashboardDto
        {
            Date = targetDate,
            TotalPendingDemands = demands.Count(d => d.Status == DemandStatus.Pending),
            TotalApprovedDemands = demands.Count(d => d.Status == DemandStatus.Approved || d.Status == DemandStatus.PartiallyApproved),
            TotalRejectedDemands = demands.Count(d => d.Status == DemandStatus.Rejected),
            TotalWasteToday = wastes.Sum(w => w.Quantity),
            BranchesOpenToday = closings.Count(c => c.IsOpened),
            BranchesClosedToday = closings.Count(c => c.IsClosed)
        };

        foreach (var branch in branches)
        {
            var closing = closings.FirstOrDefault(c => c.BranchId == branch.Id);
            var branchDemands = demands.Where(d => d.SalesBranchId == branch.Id).ToList();
            var branchWastes = wastes.Where(w => w.BranchId == branch.Id).ToList();

            dashboard.BranchSummaries.Add(new BranchSummaryDto
            {
                BranchId = branch.Id,
                BranchName = branch.Name,
                BranchType = branch.BranchType.ToString(),
                City = branch.City.ToString(),
                PendingDemandCount = branchDemands.Count(d => d.Status == DemandStatus.Pending),
                ApprovedDemandCount = branchDemands.Count(d => d.Status == DemandStatus.Approved || d.Status == DemandStatus.PartiallyApproved),
                TotalWasteQuantity = branchWastes.Sum(w => w.Quantity),
                IsDayOpened = closing?.IsOpened ?? false,
                IsDayClosed = closing?.IsClosed ?? false,
                TotalProductsInStock = closing?.Details.Count(d => d.EndOfDayCount > 0) ?? 0
            });
        }

        return ApiResponse<AdminDashboardDto>.Ok(dashboard);
    }
}
