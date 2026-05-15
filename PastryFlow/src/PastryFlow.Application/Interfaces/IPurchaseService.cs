using Microsoft.AspNetCore.Http;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Purchases;

namespace PastryFlow.Application.Interfaces;

public interface IPurchaseService
{
    Task<PagedResult<PurchaseDto>> GetPurchasesAsync(Guid branchId, PaginationParams pagination, DateTime? startDate = null, DateTime? endDate = null);
    Task<PurchaseDto> GetPurchaseByIdAsync(Guid id);
    Task<PurchaseDto> CreatePurchaseAsync(Guid branchId, Guid userId, CreatePurchaseDto dto);
    Task<PurchaseDto> UploadReceiptPhotoAsync(Guid id, IFormFile photo, string webRootPath);
    Task DeletePurchaseAsync(Guid id, Guid userId, bool isAdmin);
    
    // Admin: tüm şubeler
    Task<PagedResult<PurchaseDto>> GetAllPurchasesAsync(PaginationParams pagination, Guid? branchId = null, DateTime? startDate = null, DateTime? endDate = null);
    Task<PurchaseDto> CreateAdminPurchaseAsync(Guid userId, CreatePurchaseDto dto);
}
