using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Purchases;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers;

[ApiController]
[Route("api/purchases")]
[Authorize]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IWebHostEnvironment _env;

    public PurchasesController(IPurchaseService purchaseService, IWebHostEnvironment env)
    {
        _purchaseService = purchaseService;
        _env = env;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private Guid GetBranchId() =>
        Guid.Parse(User.FindFirstValue("BranchId")!);

    private bool IsAdmin() =>
        User.FindFirstValue(ClaimTypes.Role) == "0" ||
        User.IsInRole("Admin");

    // GET /api/purchases
    [HttpGet]
    public async Task<IActionResult> GetPurchases(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var branchId = GetBranchId();
        var pagination = new PaginationParams { PageNumber = page, PageSize = pageSize };
        var result = await _purchaseService.GetPurchasesAsync(branchId, pagination, startDate, endDate);
        return Ok(new ApiResponse<PagedResult<PurchaseDto>> { Success = true, Data = result });
    }

    // GET /api/purchases/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPurchase(Guid id)
    {
        var result = await _purchaseService.GetPurchaseByIdAsync(id);
        return Ok(new ApiResponse<PurchaseDto> { Success = true, Data = result });
    }

    // POST /api/purchases
    [HttpPost]
    public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseDto dto)
    {
        var branchId = GetBranchId();
        var userId = GetUserId();
        var result = await _purchaseService.CreatePurchaseAsync(branchId, userId, dto);
        return Ok(new ApiResponse<PurchaseDto> { Success = true, Data = result });
    }

    // POST /api/purchases/{id}/receipt-photo
    [HttpPost("{id:guid}/receipt-photo")]
    public async Task<IActionResult> UploadReceiptPhoto(Guid id, IFormFile photo)
    {
        var webRootPath = _env.ContentRootPath;
        var result = await _purchaseService.UploadReceiptPhotoAsync(id, photo, webRootPath);
        return Ok(new ApiResponse<PurchaseDto> { Success = true, Data = result });
    }

    // DELETE /api/purchases/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePurchase(Guid id)
    {
        await _purchaseService.DeletePurchaseAsync(id, GetUserId(), IsAdmin());
        return Ok(new ApiResponse<bool> { Success = true, Data = true });
    }
}
