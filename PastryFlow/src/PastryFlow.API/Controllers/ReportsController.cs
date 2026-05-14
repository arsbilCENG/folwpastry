using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Report;
using PastryFlow.Application.Interfaces;

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

    // GET /api/reports/daily-summary?branchId=&date=
    [HttpGet("daily-summary")]
    public async Task<IActionResult> GetDailySummary(
        [FromQuery] Guid? branchId,
        [FromQuery] DateOnly? date)
    {
        var targetBranchId = ResolveBranchId(branchId);
        if (targetBranchId == null)
            return BadRequest(ApiResponse<string>.Fail("Şube bilgisi bulunamadı."));

        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        var result = await _reportService.GetDailySummaryAsync(targetBranchId.Value, targetDate);
        return Ok(ApiResponse<DailySummaryReportDto>.Ok(result));
    }

    // GET /api/reports/period-summary?branchId=&startDate=&endDate=
    [HttpGet("period-summary")]
    public async Task<IActionResult> GetPeriodSummary(
        [FromQuery] Guid? branchId,
        [FromQuery] DateOnly? startDate,
        [FromQuery] DateOnly? endDate)
    {
        var targetBranchId = ResolveBranchId(branchId);
        if (targetBranchId == null)
            return BadRequest(ApiResponse<string>.Fail("Şube bilgisi bulunamadı."));

        var start = startDate ?? DateOnly.FromDateTime(
            new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        var end = endDate ?? DateOnly.FromDateTime(DateTime.Today);

        var result = await _reportService.GetPeriodSummaryAsync(targetBranchId.Value, start, end);
        return Ok(ApiResponse<PeriodSummaryReportDto>.Ok(result));
    }

    // GET /api/reports/management?startDate=&endDate=
    [HttpGet("management")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetManagementReport(
        [FromQuery] DateOnly? startDate,
        [FromQuery] DateOnly? endDate)
    {
        var start = startDate ?? DateOnly.FromDateTime(
            new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        var end = endDate ?? DateOnly.FromDateTime(DateTime.Today);

        var result = await _reportService.GetManagementReportAsync(start, end);
        return Ok(ApiResponse<ManagementReportDto>.Ok(result));
    }

    // GET /api/reports/production-report?date=2026-05-14&branchId=
    [HttpGet("production-report")]
    [Authorize(Roles = "Admin,Production")]
    public async Task<IActionResult> GetProductionReport(
        [FromQuery] DateOnly? date,
        [FromQuery] Guid? branchId)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);

        Guid productionBranchId;

        if (User.IsInRole("Admin"))
        {
            // Admin şube seçmek zorunda
            if (!branchId.HasValue)
                return BadRequest(ApiResponse<string>.Fail("Admin için şube seçimi zorunludur."));
            productionBranchId = branchId.Value;
        }
        else
        {
            // Production kullanıcısı kendi şubesini görür
            var branchClaim = User.FindFirst("BranchId")?.Value;
            if (branchClaim == null)
                return BadRequest(ApiResponse<string>.Fail("Şube bilgisi bulunamadı."));
            productionBranchId = Guid.Parse(branchClaim);
        }

        var result = await _reportService.GetProductionReportAsync(
            productionBranchId, targetDate);
        return Ok(ApiResponse<ProductionReportDto>.Ok(result));
    }

    private Guid? ResolveBranchId(Guid? queryBranchId)
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        if (role == "Admin" && queryBranchId.HasValue)
            return queryBranchId;

        var branchClaim = User.FindFirst("BranchId")?.Value;
        return branchClaim != null ? Guid.Parse(branchClaim) : queryBranchId;
    }
}
