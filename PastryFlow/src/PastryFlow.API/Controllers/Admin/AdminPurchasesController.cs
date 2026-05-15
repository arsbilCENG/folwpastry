using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Purchases;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers.Admin;

[ApiController]
[Route("api/admin/purchases")]
[Authorize(Roles = "Admin")]
public class AdminPurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IWebHostEnvironment _env;

    public AdminPurchasesController(IPurchaseService purchaseService, IWebHostEnvironment env)
    {
        _purchaseService = purchaseService;
        _env = env;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

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

    // Admin kendi adına satın alım girişi
    [HttpPost]
    public async Task<IActionResult> CreateAdminPurchase([FromBody] CreatePurchaseDto dto)
    {
        var userId = GetUserId();
        var result = await _purchaseService.CreateAdminPurchaseAsync(userId, dto);
        return Ok(new ApiResponse<PurchaseDto> { Success = true, Data = result });
    }

    [HttpPost("{id:guid}/receipt-photo")]
    public async Task<IActionResult> UploadReceiptPhoto(Guid id, IFormFile photo)
    {
        var webRootPath = _env.ContentRootPath;
        var result = await _purchaseService.UploadReceiptPhotoAsync(id, photo, webRootPath);
        return Ok(new ApiResponse<PurchaseDto> { Success = true, Data = result });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePurchase(Guid id)
    {
        var userId = GetUserId();
        await _purchaseService.DeletePurchaseAsync(id, userId, isAdmin: true);
        return Ok(new ApiResponse<bool> { Success = true, Data = true });
    }
}
