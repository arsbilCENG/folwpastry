using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;

namespace PastryFlow.Application.Interfaces;

public interface IAdminBranchService
{
    Task<ApiResponse<List<BranchListDto>>> GetBranchesAsync();
    Task<ApiResponse<BranchListDto>> GetBranchByIdAsync(Guid id);
    Task<ApiResponse<BranchListDto>> UpdateBranchAsync(Guid id, UpdateBranchDto dto);
}
