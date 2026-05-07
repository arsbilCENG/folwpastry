using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CashTransactions;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers;

[ApiController]
[Route("api/cash-transactions")]
[Authorize]
public class CashTransactionsController : ControllerBase
{
    private readonly ICashTransactionService _service;

    public CashTransactionsController(ICashTransactionService service)
    {
        _service = service;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private Guid GetBranchId() =>
        Guid.Parse(User.FindFirstValue("BranchId")!);

    // GET /api/cash-transactions — şubenin kasa hareketleri
    [HttpGet]
    public async Task<IActionResult> GetTransactions(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var branchId = GetBranchId();
        var pagination = new PaginationParams { PageNumber = page, PageSize = pageSize };
        var result = await _service.GetTransactionsAsync(branchId, pagination, startDate, endDate);
        return Ok(new ApiResponse<PagedResult<CashTransactionDto>> { Success = true, Data = result });
    }

    // GET /api/cash-transactions/summary — şube kasa özeti
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] DateTime? date = null)
    {
        var branchId = GetBranchId();
        var result = await _service.GetBranchCashSummaryAsync(branchId, date ?? DateTime.Today);
        return Ok(new ApiResponse<BranchCashSummaryDto> { Success = true, Data = result });
    }
}
