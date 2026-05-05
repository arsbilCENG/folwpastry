using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Purchases;
using PastryFlow.Application.Interfaces;

namespace PastryFlow.API.Controllers.Admin;

[ApiController]
[Route("api/admin/purchases")]
[Authorize(Roles = "Admin")]
public class AdminPurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public AdminPurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPurchases(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? branchId = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var pagination = new PaginationParams { PageNumber = page, PageSize = pageSize };
        var result = await _purchaseService.GetAllPurchasesAsync(pagination, branchId, startDate, endDate);
        return Ok(new ApiResponse<PagedResult<PurchaseDto>> { Success = true, Data = result });
    }
}
