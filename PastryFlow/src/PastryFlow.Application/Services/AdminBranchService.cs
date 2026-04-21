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

public class AdminBranchService : IAdminBranchService
{
    private readonly IPastryFlowDbContext _context;

    public AdminBranchService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<BranchListDto>>> GetBranchesAsync()
    {
        var branches = await _context.Branches
            .OrderBy(b => b.Name)
            .ToListAsync();

        var result = branches.Select(b => new BranchListDto
        {
            Id = b.Id,
            Name = b.Name,
            City = b.City.ToString(),
            BranchType = b.BranchType.ToString(),
            IsActive = b.IsActive,
            UserCount = _context.Users.Count(u => u.BranchId == b.Id && !u.IsDeleted),
            CreatedAt = b.CreatedAt
        }).ToList();

        return ApiResponse<List<BranchListDto>>.Ok(result);
    }

    public async Task<ApiResponse<BranchListDto>> GetBranchByIdAsync(Guid id)
    {
        var b = await _context.Branches.FindAsync(id);
        if (b == null) return ApiResponse<BranchListDto>.Fail("Şube bulunamadı.");

        var dto = new BranchListDto
        {
            Id = b.Id,
            Name = b.Name,
            City = b.City.ToString(),
            BranchType = b.BranchType.ToString(),
            IsActive = b.IsActive,
            UserCount = await _context.Users.CountAsync(u => u.BranchId == id && !u.IsDeleted),
            CreatedAt = b.CreatedAt
        };

        return ApiResponse<BranchListDto>.Ok(dto);
    }

    public async Task<ApiResponse<BranchListDto>> UpdateBranchAsync(Guid id, UpdateBranchDto dto)
    {
        var branch = await _context.Branches.FindAsync(id);
        if (branch == null) return ApiResponse<BranchListDto>.Fail("Şube bulunamadı.");

        if (!Enum.TryParse<City>(dto.City, true, out var cityEnum))
            return ApiResponse<BranchListDto>.Fail("Geçersiz şehir.");

        branch.Name = dto.Name;
        branch.City = cityEnum;
        branch.IsActive = dto.IsActive;
        branch.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetBranchByIdAsync(id);
    }
}
