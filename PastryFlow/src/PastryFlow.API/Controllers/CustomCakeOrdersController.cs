using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/custom-cake-orders")]
public class CustomCakeOrdersController : ControllerBase
{
    private readonly ICustomCakeOrderService _orderService;
    private readonly IWebHostEnvironment _environment;

    public CustomCakeOrdersController(ICustomCakeOrderService orderService, IWebHostEnvironment environment)
    {
        _orderService = orderService;
        _environment = environment;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomCakeOrderDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var branchIdString = User.FindFirst("BranchId")?.Value;
        
        if (!Guid.TryParse(userIdString, out var userId) || !Guid.TryParse(branchIdString, out var branchId))
            return Unauthorized();

        var result = await _orderService.CreateAsync(dto, userId, branchId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CustomCakeOrderStatus? status)
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var branchIdString = User.FindFirst("BranchId")?.Value;
        
        if (!Guid.TryParse(branchIdString, out var branchId) && role != "Admin")
            return Unauthorized();

        if (role == "Admin")
        {
            var result = await _orderService.GetAllAsync(status);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        else if (role == "Production")
        {
            var result = await _orderService.GetByProductionBranchAsync(branchId, status);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        else // Sales
        {
            var result = await _orderService.GetByBranchAsync(branchId, status);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateCakeOrderStatusDto dto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _orderService.UpdateStatusAsync(id, dto, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("{id}/reference-photo")]
    public async Task<IActionResult> UploadReferencePhoto(Guid id, [FromForm] IFormFile photo)
    {
        var result = await _orderService.UploadReferencePhotoAsync(id, photo, _environment.ContentRootPath);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        var result = await _orderService.CancelAsync(id, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
