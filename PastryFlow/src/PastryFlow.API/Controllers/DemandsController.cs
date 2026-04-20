using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.Demand;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;
using System.Security.Claims;
using PastryFlow.Application.Common;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DemandsController : ControllerBase
{
    private readonly IDemandService _demandService;

    public DemandsController(IDemandService demandService)
    {
        _demandService = demandService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDemand(CreateDemandDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _demandService.CreateDemandAsync(userId, dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetDemands(
        [FromQuery] Guid? branchId,
        [FromQuery] Guid? productionBranchId,
        [FromQuery] string? status,
        [FromQuery] string? date)
    {
        DemandStatus? dStatus = null;
        if (Enum.TryParse<DemandStatus>(status, true, out var parsedStatus))
            dStatus = parsedStatus;

        DateOnly? dDate = null;
        if (DateOnly.TryParse(date, out var parsedDate))
            dDate = parsedDate;

        var result = await _demandService.GetDemandsAsync(branchId, productionBranchId, dStatus, dDate);
        return Ok(result);
    }

    [HttpGet("last")]
    public async Task<IActionResult> GetLastDemand([FromQuery] Guid salesBranchId, [FromQuery] Guid productionBranchId)
    {
        var result = await _demandService.GetLastDemandAsync(salesBranchId, productionBranchId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDemand(Guid id)
    {
        var result = await _demandService.GetDemandByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPatch("{id}/review")]
    public async Task<IActionResult> ReviewDemand(Guid id, ReviewDemandDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        if (dto.ReviewedByUserId == Guid.Empty)
            dto.ReviewedByUserId = userId;

        var result = await _demandService.ReviewDemandAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPatch("{id}/deliver")]
    public async Task<IActionResult> DeliverDemand(Guid id, DeliverDemandDto dto)
    {
        var result = await _demandService.DeliverDemandAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPatch("{id}/receive")]
    public async Task<IActionResult> ReceiveDemand(Guid id, ReceiveDemandDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        if (dto.ReceivedByUserId == Guid.Empty)
            dto.ReceivedByUserId = userId;

        var result = await _demandService.ReceiveDemandAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
