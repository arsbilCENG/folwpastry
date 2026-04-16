using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Category;
using PastryFlow.Application.DTOs.Product;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class ProductService : IProductService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(IPastryFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ProductDto>>> GetProductsAsync(Guid? branchId = null, Guid? categoryId = null, ProductType? productType = null)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductionBranch)
            .Where(p => p.IsActive);

        if (branchId.HasValue)
        {
            query = query.Where(p => p.ProductionBranchId == branchId.Value || p.ProductionBranchId == null);
        }
        
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (productType.HasValue)
        {
            query = query.Where(p => p.ProductType == productType.Value);
        }

        var products = await query.OrderBy(p => p.Category.SortOrder).ThenBy(p => p.Name).ToListAsync();
        return ApiResponse<List<ProductDto>>.Ok(_mapper.Map<List<ProductDto>>(products));
    }

    public async Task<ApiResponse<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync(Guid? branchId = null, ProductType? productType = null)
    {
        var productQuery = _context.Products.Where(p => p.IsActive);
        
        if (branchId.HasValue)
            productQuery = productQuery.Where(p => p.ProductionBranchId == branchId.Value || p.ProductionBranchId == null);
            
        if (productType.HasValue)
            productQuery = productQuery.Where(p => p.ProductType == productType.Value);
            
        var categories = await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.SortOrder)
            .Select(c => new CategoryWithProductsDto
            {
                Id = c.Id,
                Name = c.Name,
                SortOrder = c.SortOrder,
                Products = productQuery.Where(p => p.CategoryId == c.Id)
                                       .OrderBy(p => p.Name)
                                       .Select(p => new ProductDto
                                       {
                                           Id = p.Id,
                                           Name = p.Name,
                                           CategoryId = p.CategoryId,
                                           CategoryName = c.Name,
                                           ProductionBranchId = p.ProductionBranchId,
                                           ProductType = p.ProductType,
                                           Unit = p.Unit,
                                           UnitPrice = p.UnitPrice,
                                           IsActive = p.IsActive
                                       }).ToList()
            })
            .ToListAsync();
            
        // Filter out categories with no products
        categories = categories.Where(c => c.Products.Any()).ToList();
        
        return ApiResponse<List<CategoryWithProductsDto>>.Ok(categories);
    }
}
