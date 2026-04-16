using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Branch;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface IBranchService
{
    Task<ApiResponse<List<BranchDto>>> GetBranchesAsync(BranchType? type = null);
    Task<ApiResponse<BranchDto>> GetBranchByIdAsync(Guid id);
}
