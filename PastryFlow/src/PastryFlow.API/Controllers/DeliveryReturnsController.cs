using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/delivery-returns")]
public class DeliveryReturnsController : ControllerBase
{
    private readonly IDeliveryReturnService _returnService;

    public DeliveryReturnsController(IDeliveryReturnService returnService)
    {
        _returnService = returnService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReturns(
        [FromQuery] Guid? branchId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var result = await _returnService.GetReturnsAsync(branchId, startDate, endDate);
        return Ok(result);
    }

    [HttpGet("demand/{demandId}")]
    public async Task<IActionResult> GetReturnsByDemand(Guid demandId)
    {
        var result = await _returnService.GetReturnsByDemandAsync(demandId);
        return Ok(result);
    }

    [Authorize(Roles = "Production")]
    [HttpPut("{id}/acknowledge")]
    public async Task<IActionResult> AcknowledgeReturn(Guid id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _returnService.AcknowledgeReturnAsync(id, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
