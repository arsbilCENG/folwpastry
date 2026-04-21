using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class AdminProductService : IAdminProductService
{
    private readonly IPastryFlowDbContext _context;

    public AdminProductService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<PagedResult<ProductListDto>>> GetProductsAsync(Guid? categoryId, Guid? productionBranchId, string? search, string? unitType, PaginationParams pagination)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductionBranch)
            .OrderBy(p => p.Category.SortOrder)
            .ThenBy(p => p.SortOrder)
            .ThenBy(p => p.Name)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (productionBranchId.HasValue)
        {
            query = query.Where(p => p.ProductionBranchId == productionBranchId.Value);
        }

        if (!string.IsNullOrEmpty(unitType) && Enum.TryParse<UnitType>(unitType, true, out var unitEnum))
        {
            query = query.Where(p => p.Unit == unitEnum);
        }

        if (!string.IsNullOrEmpty(search))
        {
            var searchPattern = $"%{search}%";
            query = query.Where(p => EF.Functions.Like(p.Name, searchPattern));
        }

        var pagedProducts = await query.ToPagedResultAsync(pagination);

        var result = pagedProducts.Map(p => new ProductListDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,
            ProductionBranchId = p.ProductionBranchId,
            ProductionBranchName = p.ProductionBranch != null ? p.ProductionBranch.Name : null,
            UnitType = p.Unit.ToString(),
            UnitPrice = p.UnitPrice,
            IsRawMaterial = p.ProductType == ProductType.RawMaterial,
            SortOrder = p.SortOrder,
            CreatedAt = p.CreatedAt
        });

        return ApiResponse<PagedResult<ProductListDto>>.Ok(result);
    }

    public async Task<ApiResponse<ProductListDto>> GetProductByIdAsync(Guid id)
    {
        var p = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductionBranch)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null) return ApiResponse<ProductListDto>.Fail("Ürün bulunamadı.");

        var dto = new ProductListDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,
            ProductionBranchId = p.ProductionBranchId,
            ProductionBranchName = p.ProductionBranch != null ? p.ProductionBranch.Name : null,
            UnitType = p.Unit.ToString(),
            UnitPrice = p.UnitPrice,
            IsRawMaterial = p.ProductType == ProductType.RawMaterial,
            SortOrder = p.SortOrder,
            CreatedAt = p.CreatedAt
        };

        return ApiResponse<ProductListDto>.Ok(dto);
    }

    public async Task<ApiResponse<ProductListDto>> CreateProductAsync(CreateProductDto dto)
    {
        // Validations
        var exists = await _context.Products.AnyAsync(p => p.CategoryId == dto.CategoryId && p.Name.ToLower() == dto.Name.ToLower());
        if (exists) return ApiResponse<ProductListDto>.Fail("Bu kategoride aynı isimde ürün mevcut.");

        var category = await _context.Categories.FindAsync(dto.CategoryId);
        if (category == null || category.IsDeleted) return ApiResponse<ProductListDto>.Fail("Kategori bulunamadı.");

        if (dto.ProductionBranchId.HasValue)
        {
            var branch = await _context.Branches.FindAsync(dto.ProductionBranchId.Value);
            if (branch == null || branch.IsDeleted || branch.BranchType != BranchType.Production)
                return ApiResponse<ProductListDto>.Fail("Geçersiz üretim şubesi.");
        }

        if (dto.IsRawMaterial && dto.ProductionBranchId.HasValue)
            return ApiResponse<ProductListDto>.Fail("Hammadde ürünleri üretim şubesine atanamaz.");

        if (!Enum.TryParse<UnitType>(dto.UnitType, true, out var unitEnum))
            return ApiResponse<ProductListDto>.Fail("Geçersiz birim tipi.");

        var product = new Product
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            ProductionBranchId = dto.ProductionBranchId,
            Unit = unitEnum,
            UnitPrice = dto.UnitPrice,
            ProductType = dto.IsRawMaterial ? ProductType.RawMaterial : ProductType.FinishedProduct,
            SortOrder = dto.SortOrder
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return await GetProductByIdAsync(product.Id);
    }

    public async Task<ApiResponse<ProductListDto>> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return ApiResponse<ProductListDto>.Fail("Ürün bulunamadı.");

        var exists = await _context.Products.AnyAsync(p => p.CategoryId == dto.CategoryId && p.Name.ToLower() == dto.Name.ToLower() && p.Id != id);
        if (exists) return ApiResponse<ProductListDto>.Fail("Bu kategoride aynı isimde ürün mevcut.");

        var category = await _context.Categories.FindAsync(dto.CategoryId);
        if (category == null || category.IsDeleted) return ApiResponse<ProductListDto>.Fail("Kategori bulunamadı.");

        if (dto.ProductionBranchId.HasValue)
        {
            var branch = await _context.Branches.FindAsync(dto.ProductionBranchId.Value);
            if (branch == null || branch.IsDeleted || branch.BranchType != BranchType.Production)
                return ApiResponse<ProductListDto>.Fail("Geçersiz üretim şubesi.");
        }

        if (dto.IsRawMaterial && dto.ProductionBranchId.HasValue)
            return ApiResponse<ProductListDto>.Fail("Hammadde ürünleri üretim şubesine atanamaz.");

        if (!Enum.TryParse<UnitType>(dto.UnitType, true, out var unitEnum))
            return ApiResponse<ProductListDto>.Fail("Geçersiz birim tipi.");

        product.Name = dto.Name;
        product.CategoryId = dto.CategoryId;
        product.ProductionBranchId = dto.ProductionBranchId;
        product.Unit = unitEnum;
        product.UnitPrice = dto.UnitPrice;
        product.ProductType = dto.IsRawMaterial ? ProductType.RawMaterial : ProductType.FinishedProduct;
        product.SortOrder = dto.SortOrder;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetProductByIdAsync(id);
    }

    public async Task<ApiResponse<string>> DeleteProductAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return ApiResponse<string>.Fail("Ürün bulunamadı.");

        var activeStockBranches = await _context.DayClosingDetails
            .Include(d => d.DayClosing)
            .Where(d => d.ProductId == id && d.EndOfDayCount > 0 && !d.DayClosing.IsClosed)
            .Select(d => d.DayClosing.Branch.Name)
            .Distinct()
            .ToListAsync();

        product.IsDeleted = true;
        product.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        string message = "Ürün silindi.";
        if (activeStockBranches.Any())
        {
            message += $" UYARI: Bu ürünün {activeStockBranches.Count} şubede ({string.Join(", ", activeStockBranches)}) aktif stok kaydı bulunmaktadır.";
        }

        return ApiResponse<string>.Ok(message);
    }
}
