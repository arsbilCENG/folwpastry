using System;
using System.Threading;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;

namespace PastryFlow.Application.Interfaces;

public interface IAdminProductService
{
    Task<ApiResponse<PagedResult<ProductListDto>>> GetProductsAsync(Guid? categoryId, Guid? productionBranchId, string? search, string? unitType, PaginationParams pagination);
    Task<ApiResponse<ProductListDto>> GetProductByIdAsync(Guid id);
    Task<ApiResponse<ProductListDto>> CreateProductAsync(CreateProductDto dto);
    Task<ApiResponse<ProductListDto>> UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task<ApiResponse<string>> DeleteProductAsync(Guid id);
}
