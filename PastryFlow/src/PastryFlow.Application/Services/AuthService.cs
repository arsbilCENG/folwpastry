using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Auth;
using PastryFlow.Application.Interfaces;
using System.Text;

namespace PastryFlow.Application.Services;

public class AuthService : IAuthService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(IPastryFlowDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .Include(u => u.Branch)
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return ApiResponse<LoginResponseDto>.Fail("Hatalı e-posta veya şifre.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("FullName", user.FullName)
        };

        if (user.BranchId.HasValue)
        {
            claims.Add(new Claim("BranchId", user.BranchId.Value.ToString()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpirationInMinutes"]!)),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        var response = new LoginResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = Guid.NewGuid().ToString(), // Simplified for MVP
            User = new CurrentUserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                BranchId = user.BranchId,
                BranchName = user.Branch?.Name
            }
        };

        return ApiResponse<LoginResponseDto>.Ok(response);
    }

    public async Task<ApiResponse<CurrentUserDto>> GetCurrentUserAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Branch)
            .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);

        if (user == null)
            return ApiResponse<CurrentUserDto>.Fail("Kullanıcı bulunamadı.");

        return ApiResponse<CurrentUserDto>.Ok(new CurrentUserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString(),
            BranchId = user.BranchId,
            BranchName = user.Branch?.Name
        });
    }
}
