using System;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;

namespace PastryFlow.Application.Interfaces;

public interface IAdminUserService
{
    Task<ApiResponse<PagedResult<UserListDto>>> GetUsersAsync(string? role, Guid? branchId, string? search, PaginationParams pagination);
    Task<ApiResponse<UserListDto>> GetUserByIdAsync(Guid id);
    Task<ApiResponse<UserListDto>> UpdateUserAsync(Guid id, UpdateUserDto dto);
    Task<ApiResponse<string>> ResetPasswordAsync(Guid id, ResetPasswordDto dto);
    Task<ApiResponse<string>> DeleteUserAsync(Guid id, Guid currentUserId);
}
