using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;

namespace PastryFlow.Application.Interfaces;

public interface IAdminCategoryService
{
    Task<ApiResponse<List<CategoryListDto>>> GetCategoriesAsync();
    Task<ApiResponse<CategoryListDto>> GetCategoryByIdAsync(Guid id);
    Task<ApiResponse<CategoryListDto>> CreateCategoryAsync(CreateCategoryDto dto);
    Task<ApiResponse<CategoryListDto>> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);
    Task<ApiResponse<string>> DeleteCategoryAsync(Guid id);
}
