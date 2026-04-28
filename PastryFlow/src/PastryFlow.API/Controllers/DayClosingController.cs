using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.DayClosing;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PastryFlow.Application.Common;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/day-closing")]
public class DayClosingController : ControllerBase
{
    private readonly IDayClosingService _dayClosingService;
    private readonly IWebHostEnvironment _environment;

    public DayClosingController(IDayClosingService dayClosingService, IWebHostEnvironment environment)
    {
        _dayClosingService = dayClosingService;
        _environment = environment;
    }

    [HttpPost("count")]
    public async Task<IActionResult> SaveCount(CountInputDto dto)
    {
        var result = await _dayClosingService.SaveCountAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("carry-over")]
    public async Task<IActionResult> SaveCarryOver(CarryOverInputDto dto)
    {
        var result = await _dayClosingService.SaveCarryOverAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("expected-cash")]
    public async Task<IActionResult> GetExpectedCash([FromQuery] Guid branchId, [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Geçersiz tarih formatı.");

        var result = await _dayClosingService.CalculateExpectedCashAsync(branchId, parsedDate);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{dayClosingId}/cash-count")]
    public async Task<IActionResult> SubmitCashCount(Guid dayClosingId, [FromBody] CashCountDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _dayClosingService.SubmitCashCountAsync(dayClosingId, dto, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{dayClosingId}/receipt-photo")]
    public async Task<IActionResult> UploadReceiptPhoto(Guid dayClosingId, [FromForm] IFormFile photo)
    {
        var photoUrl = await FileUploadHelper.SaveFileAsync(photo, "day-closing/receipts", _environment.ContentRootPath);
        var result = await _dayClosingService.UpdateReceiptPhotoAsync(dayClosingId, photoUrl);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{dayClosingId}/counter-photo")]
    public async Task<IActionResult> UploadCounterPhoto(Guid dayClosingId, [FromForm] IFormFile photo)
    {
        var photoUrl = await FileUploadHelper.SaveFileAsync(photo, "day-closing/counters", _environment.ContentRootPath);
        var result = await _dayClosingService.UpdateCounterPhotoAsync(dayClosingId, photoUrl);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    public class CloseDayRequest
    {
        public Guid BranchId { get; set; }
        public DateOnly Date { get; set; }
        public Guid ClosedByUserId { get; set; }
    }

    [HttpPost("close")]
    public async Task<IActionResult> CloseDay(CloseDayRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var closedBy = request.ClosedByUserId == Guid.Empty ? userId : request.ClosedByUserId;

        var result = await _dayClosingService.CloseDayAsync(request.BranchId, request.Date, closedBy);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] Guid branchId, [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Geçersiz tarih formatı.");

        var result = await _dayClosingService.GetSummaryAsync(branchId, parsedDate);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
