using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Category;
using PastryFlow.Application.DTOs.Product;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<List<ProductDto>>> GetProductsAsync(Guid? branchId = null, Guid? categoryId = null, ProductType? productType = null);
    Task<ApiResponse<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync(Guid? branchId = null, ProductType? productType = null);
}
