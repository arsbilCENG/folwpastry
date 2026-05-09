using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Reports;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("daily-sales")]
    public async Task<IActionResult> GetDailySales([FromQuery] string date, [FromQuery] Guid? branchId)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Geçersiz tarih formatı.");

        var currentUserBranchId = GetUserBranchId();
        var userRole = GetUserRole();

        if (userRole != UserRole.Admin)
        {
            if (branchId.HasValue && branchId != currentUserBranchId)
                return Forbid();
            branchId = currentUserBranchId;
        }

        var result = await _reportService.GetDailySalesReportAsync(parsedDate, branchId);
        return Ok(result);
    }

    [HttpGet("waste-summary")]
    public async Task<IActionResult> GetWasteSummary([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] Guid? branchId, [FromQuery] Guid? categoryId)
    {
        if (!DateOnly.TryParse(startDate, out var start) || !DateOnly.TryParse(endDate, out var end))
            return BadRequest("Geçersiz tarih formatı.");

        var currentUserBranchId = GetUserBranchId();
        var userRole = GetUserRole();

        if (userRole != UserRole.Admin)
        {
            if (branchId.HasValue && branchId != currentUserBranchId)
                return Forbid();
            branchId = currentUserBranchId;
        }

        var result = await _reportService.GetWasteSummaryReportAsync(start, end, branchId, categoryId);
        return Ok(result);
    }

    [HttpGet("demand-summary")]
    public async Task<IActionResult> GetDemandSummary([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] Guid? branchId)
    {
        if (!DateOnly.TryParse(startDate, out var start) || !DateOnly.TryParse(endDate, out var end))
            return BadRequest("Geçersiz tarih formatı.");

        var currentUserBranchId = GetUserBranchId();
        var userRole = GetUserRole();

        if (userRole != UserRole.Admin)
        {
            if (branchId.HasValue && branchId != currentUserBranchId)
                return Forbid();
            branchId = currentUserBranchId;
        }

        var result = await _reportService.GetDemandSummaryReportAsync(start, end, branchId);
        return Ok(result);
    }

    [HttpGet("branch-comparison")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBranchComparison([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] string metric)
    {
        if (!DateOnly.TryParse(startDate, out var start) || !DateOnly.TryParse(endDate, out var end))
            return BadRequest("Geçersiz tarih formatı.");

        var result = await _reportService.GetBranchComparisonReportAsync(start, end, metric);
        return Ok(result);
    }

    // GET /api/reports/purchases?branchId=...&startDate=...&endDate=...
    [HttpGet("purchases")]
    [Authorize(Roles = "Admin,Sales,Production")]
    public async Task<IActionResult> GetPurchaseReport(
        [FromQuery] Guid? branchId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var end = endDate ?? DateTime.Today;
        
        var currentUserBranchId = GetUserBranchId();
        var userRole = GetUserRole();

        if (userRole != UserRole.Admin)
        {
            branchId = currentUserBranchId;
        }

        var result = await _reportService.GetPurchaseReportAsync(branchId, start, end);
        return Ok(ApiResponse<PurchaseReportDto>.Ok(result));
    }

    // GET /api/reports/cash-transactions?branchId=...&startDate=...&endDate=...
    [HttpGet("cash-transactions")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCashTransactionReport(
        [FromQuery] Guid? branchId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var start = startDate ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var end = endDate ?? DateTime.Today;
        var result = await _reportService.GetCashTransactionReportAsync(branchId, start, end);
        return Ok(ApiResponse<CashTransactionReportDto>.Ok(result));
    }

    private Guid? GetUserBranchId()
    {
        var branchIdStr = User.FindFirst("BranchId")?.Value;
        return Guid.TryParse(branchIdStr, out var branchId) ? branchId : null;
    }

    private UserRole? GetUserRole()
    {
        var roleStr = User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<UserRole>(roleStr, true, out var role) ? role : null;
    }
}
