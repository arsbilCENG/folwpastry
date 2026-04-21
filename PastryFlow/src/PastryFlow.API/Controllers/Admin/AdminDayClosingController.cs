using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.Interfaces;

namespace PastryFlow.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/day-closing")]
public class AdminDayClosingController : ControllerBase
{
    private readonly IAdminDayClosingService _adminDayClosingService;

    public AdminDayClosingController(IAdminDayClosingService adminDayClosingService)
    {
        _adminDayClosingService = adminDayClosingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDayClosing([FromQuery] Guid branchId, [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Geçersiz tarih formatı.");

        var result = await _adminDayClosingService.GetBranchDayClosingAsync(branchId, parsedDate);
        return Ok(result);
    }

    [HttpPut("{dayClosingId}/correct")]
    public async Task<IActionResult> CorrectDayClosing(Guid dayClosingId, DayClosingCorrectionDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _adminDayClosingService.CorrectDayClosingDetailAsync(dayClosingId, dto, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
