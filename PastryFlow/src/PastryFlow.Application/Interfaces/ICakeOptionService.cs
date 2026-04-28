using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface ICakeOptionService
{
    Task<ApiResponse<List<CakeOptionDto>>> GetAllAsync(CakeOptionType? optionType = null);
    Task<ApiResponse<CakeOptionDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<CakeOptionDto>> CreateAsync(CreateCakeOptionDto dto);
    Task<ApiResponse<CakeOptionDto>> UpdateAsync(Guid id, UpdateCakeOptionDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
