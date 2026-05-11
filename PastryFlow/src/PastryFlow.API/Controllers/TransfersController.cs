using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Transfer;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PastryFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransfersController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
    }

    private Guid GetBranchId()
    {
        var claim = User.FindFirst("BranchId");
        return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTransferRequest request)
    {
        var result = await _transferService.CreateTransferAsync(request, GetBranchId(), GetUserId());
        return Ok(ApiResponse<TransferDto>.Ok(result));
    }

    [HttpPut("{id}/receive")]
    [Authorize]
    public async Task<IActionResult> Receive(Guid id)
    {
        var result = await _transferService.ReceiveTransferAsync(id, GetBranchId(), GetUserId());
        return Ok(ApiResponse<TransferDto>.Ok(result));
    }

    [HttpPut("{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelTransferRequest request)
    {
        await _transferService.CancelTransferAsync(id, GetBranchId(), GetUserId(), request);
        return Ok(ApiResponse<bool>.Ok(true, "Transfer başarıyla iptal edildi."));
    }

    [HttpGet("outgoing")]
    [Authorize]
    public async Task<IActionResult> GetOutgoing([FromQuery] TransferStatus? status)
    {
        var result = await _transferService.GetOutgoingTransfersAsync(GetBranchId(), status);
        return Ok(ApiResponse<System.Collections.Generic.List<TransferDto>>.Ok(result));
    }

    [HttpGet("incoming")]
    [Authorize]
    public async Task<IActionResult> GetIncoming([FromQuery] TransferStatus? status)
    {
        var result = await _transferService.GetIncomingTransfersAsync(GetBranchId(), status);
        return Ok(ApiResponse<System.Collections.Generic.List<TransferDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _transferService.GetTransferByIdAsync(id);
        return Ok(ApiResponse<TransferDto>.Ok(result));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] TransferStatus? status)
    {
        var result = await _transferService.GetAllTransfersAsync(status);
        return Ok(ApiResponse<System.Collections.Generic.List<TransferDto>>.Ok(result));
    }
}
