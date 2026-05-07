using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CashTransactions;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers.Admin;

[ApiController]
[Route("api/admin/cash-transactions")]
[Authorize(Roles = "Admin")]
public class AdminCashTransactionsController : ControllerBase
{
    private readonly ICashTransactionService _service;

    public AdminCashTransactionsController(ICashTransactionService service)
    {
        _service = service;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // POST /api/admin/cash-transactions — para çek/yatır
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(
        [FromBody] CreateCashTransactionDto dto)
    {
        var result = await _service.CreateTransactionAsync(GetUserId(), dto);
        return Ok(new ApiResponse<CashTransactionDto> { Success = true, Data = result });
    }

    // GET /api/admin/cash-transactions — tüm şubeler
    [HttpGet]
    public async Task<IActionResult> GetAllTransactions(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? branchId = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var pagination = new PaginationParams { PageNumber = page, PageSize = pageSize };
        var result = await _service.GetAllTransactionsAsync(pagination, branchId, startDate, endDate);
        return Ok(new ApiResponse<PagedResult<CashTransactionDto>> { Success = true, Data = result });
    }

    // GET /api/admin/cash-transactions/summaries — tüm şube özetleri
    [HttpGet("summaries")]
    public async Task<IActionResult> GetSummaries([FromQuery] DateTime? date = null)
    {
        var result = await _service.GetAllBranchCashSummariesAsync(date ?? DateTime.Today);
        return Ok(new ApiResponse<List<BranchCashSummaryDto>> { Success = true, Data = result });
    }

    // DELETE /api/admin/cash-transactions/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        await _service.DeleteTransactionAsync(id, GetUserId());
        return Ok(new ApiResponse<bool> { Success = true, Data = true });
    }
}
