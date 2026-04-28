using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class CakeOptionService : ICakeOptionService
{
    private readonly IPastryFlowDbContext _context;

    public CakeOptionService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    private CakeOptionDto MapToDto(CakeOption entity)
    {
        return new CakeOptionDto
        {
            Id = entity.Id,
            Name = entity.Name,
            OptionType = entity.OptionType.ToString(),
            SortOrder = entity.SortOrder,
            IsActive = entity.IsActive
        };
    }

    public async Task<ApiResponse<List<CakeOptionDto>>> GetAllAsync(CakeOptionType? optionType = null)
    {
        var query = _context.CakeOptions.AsQueryable();

        if (optionType.HasValue)
        {
            query = query.Where(o => o.OptionType == optionType.Value);
        }

        var options = await query
            .OrderBy(o => o.OptionType)
            .ThenBy(o => o.SortOrder)
            .ThenBy(o => o.Name)
            .ToListAsync();

        return ApiResponse<List<CakeOptionDto>>.Ok(options.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<CakeOptionDto>> GetByIdAsync(Guid id)
    {
        var option = await _context.CakeOptions.FindAsync(id);
        if (option == null) return ApiResponse<CakeOptionDto>.Fail("Seçenek bulunamadı.");

        return ApiResponse<CakeOptionDto>.Ok(MapToDto(option));
    }

    public async Task<ApiResponse<CakeOptionDto>> CreateAsync(CreateCakeOptionDto dto)
    {
        var exists = await _context.CakeOptions
            .AnyAsync(o => o.OptionType == dto.OptionType && o.Name.ToLower() == dto.Name.ToLower());
            
        if (exists)
            return ApiResponse<CakeOptionDto>.Fail("Bu isimde bir seçenek bu kategori için zaten mevcut.");

        var option = new CakeOption
        {
            Name = dto.Name,
            OptionType = dto.OptionType,
            SortOrder = dto.SortOrder,
            IsActive = true
        };

        _context.CakeOptions.Add(option);
        await _context.SaveChangesAsync();

        return ApiResponse<CakeOptionDto>.Ok(MapToDto(option), "Seçenek başarıyla oluşturuldu.");
    }

    public async Task<ApiResponse<CakeOptionDto>> UpdateAsync(Guid id, UpdateCakeOptionDto dto)
    {
        var option = await _context.CakeOptions.FindAsync(id);
        if (option == null) return ApiResponse<CakeOptionDto>.Fail("Seçenek bulunamadı.");

        var exists = await _context.CakeOptions
            .AnyAsync(o => o.Id != id && o.OptionType == option.OptionType && o.Name.ToLower() == dto.Name.ToLower());
            
        if (exists)
            return ApiResponse<CakeOptionDto>.Fail("Bu isimde bir seçenek bu kategori için zaten mevcut.");

        option.Name = dto.Name;
        option.SortOrder = dto.SortOrder;
        option.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return ApiResponse<CakeOptionDto>.Ok(MapToDto(option), "Seçenek güncellendi.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var option = await _context.CakeOptions.FindAsync(id);
        if (option == null) return ApiResponse<bool>.Fail("Seçenek bulunamadı.");

        // Soft delete (BaseEntity handled by DbContext SaveChangesAsync if implemented, otherwise manual)
        // Check if there's any order using this.
        bool isUsed = await _context.CustomCakeOrders.AnyAsync(o => 
            o.CakeTypeId == id || o.InnerCreamId == id || o.OuterCreamId == id);
            
        if (isUsed)
        {
            // Just mark inactive, don't hard delete to preserve history
            option.IsActive = false;
            option.IsDeleted = true; // Set soft delete flag
            await _context.SaveChangesAsync();
            return ApiResponse<bool>.Ok(true, "Seçenek kullanıldığı için pasife çekildi ve silindi olarak işaretlendi.");
        }

        option.IsDeleted = true;
        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Seçenek başarıyla silindi.");
    }
}
