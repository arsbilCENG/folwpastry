using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Services;

public class AdminCategoryService : IAdminCategoryService
{
    private readonly IPastryFlowDbContext _context;

    public AdminCategoryService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<CategoryListDto>>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.SortOrder)
            .Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = _context.Products.Count(p => p.CategoryId == c.Id && !p.IsDeleted),
                SortOrder = c.SortOrder,
                CreatedAt = c.CreatedAt
            }).ToListAsync();

        return ApiResponse<List<CategoryListDto>>.Ok(categories);
    }

    public async Task<ApiResponse<CategoryListDto>> GetCategoryByIdAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return ApiResponse<CategoryListDto>.Fail("Kategori bulunamadı.");

        var dto = new CategoryListDto
        {
            Id = category.Id,
            Name = category.Name,
            ProductCount = await _context.Products.CountAsync(p => p.CategoryId == id && !p.IsDeleted),
            SortOrder = category.SortOrder,
            CreatedAt = category.CreatedAt
        };

        return ApiResponse<CategoryListDto>.Ok(dto);
    }

    public async Task<ApiResponse<CategoryListDto>> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var nameLower = dto.Name.ToTurkishLower();
        var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == nameLower);
        if (exists) return ApiResponse<CategoryListDto>.Fail("Bu isimde bir kategori zaten mevcut.");

        var category = new Category
        {
            Name = dto.Name,
            SortOrder = dto.SortOrder
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return await GetCategoryByIdAsync(category.Id);
    }

    public async Task<ApiResponse<CategoryListDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return ApiResponse<CategoryListDto>.Fail("Kategori bulunamadı.");

        var nameLower = dto.Name.ToTurkishLower();
        var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == nameLower && c.Id != id);
        if (exists) return ApiResponse<CategoryListDto>.Fail("Bu isimde bir kategori zaten mevcut.");

        category.Name = dto.Name;
        category.SortOrder = dto.SortOrder;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetCategoryByIdAsync(id);
    }

    public async Task<ApiResponse<string>> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return ApiResponse<string>.Fail("Kategori bulunamadı.");

        var productCount = await _context.Products.CountAsync(p => p.CategoryId == id && !p.IsDeleted);
        if (productCount > 0)
            return ApiResponse<string>.Fail($"Bu kategoride {productCount} adet ürün bulunmaktadır. Önce ürünleri başka kategoriye taşıyınız veya siliniz.");

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Kategori silindi.");
    }
}
