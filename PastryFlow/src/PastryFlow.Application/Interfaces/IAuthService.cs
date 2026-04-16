using System;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Auth;

namespace PastryFlow.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<ApiResponse<CurrentUserDto>> GetCurrentUserAsync(Guid userId);
}
