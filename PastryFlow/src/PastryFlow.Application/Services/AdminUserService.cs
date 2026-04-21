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

public class AdminUserService : IAdminUserService
{
    private readonly IPastryFlowDbContext _context;

    public AdminUserService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<PagedResult<UserListDto>>> GetUsersAsync(string? role, Guid? branchId, string? search, PaginationParams pagination)
    {
        var query = _context.Users
            .Include(u => u.Branch)
            .OrderByDescending(u => u.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, true, out var roleEnum))
        {
            query = query.Where(u => u.Role == roleEnum);
        }

        if (branchId.HasValue)
        {
            query = query.Where(u => u.BranchId == branchId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            var searchPattern = $"%{search}%";
            query = query.Where(u => EF.Functions.Like(u.FullName, searchPattern) || EF.Functions.Like(u.Email, searchPattern));
        }

        var pagedUsers = await query.ToPagedResultAsync(pagination);
        
        var result = pagedUsers.Map(u => new UserListDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            Role = u.Role.ToString(),
            BranchId = u.BranchId,
            BranchName = u.Branch != null ? u.Branch.Name : null,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt
        });

        return ApiResponse<PagedResult<UserListDto>>.Ok(result);
    }

    public async Task<ApiResponse<UserListDto>> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Branch)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return ApiResponse<UserListDto>.Fail("Kullanıcı bulunamadı.");

        var dto = new UserListDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString(),
            BranchId = user.BranchId,
            BranchName = user.Branch?.Name,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

        return ApiResponse<UserListDto>.Ok(dto);
    }

    public async Task<ApiResponse<UserListDto>> UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return ApiResponse<UserListDto>.Fail("Kullanıcı bulunamadı.");

        var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id);
        if (emailExists) return ApiResponse<UserListDto>.Fail("Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");

        if (!Enum.TryParse<UserRole>(dto.Role, true, out var roleEnum))
            return ApiResponse<UserListDto>.Fail("Geçersiz rol.");

        if (dto.BranchId.HasValue)
        {
            var branchExists = await _context.Branches.AnyAsync(b => b.Id == dto.BranchId.Value);
            if (!branchExists) return ApiResponse<UserListDto>.Fail("Geçersiz şube.");
        }

        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.Role = roleEnum;
        user.BranchId = dto.BranchId;
        user.IsActive = dto.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetUserByIdAsync(id);
    }

    public async Task<ApiResponse<string>> ResetPasswordAsync(Guid id, ResetPasswordDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return ApiResponse<string>.Fail("Kullanıcı bulunamadı.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Şifre başarıyla güncellendi.");
    }

    public async Task<ApiResponse<string>> DeleteUserAsync(Guid id, Guid currentUserId)
    {
        if (id == currentUserId) return ApiResponse<string>.Fail("Kendi hesabınızı silemezsiniz.");

        var user = await _context.Users.FindAsync(id);
        if (user == null) return ApiResponse<string>.Fail("Kullanıcı bulunamadı.");

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.IsActive = false;

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Kullanıcı silindi.");
    }
}
