using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Waste;

namespace PastryFlow.Application.Interfaces;

public interface IWasteService
{
    Task<ApiResponse<WasteDto>> CreateWasteAsync(CreateWasteDto dto, Guid createdByUserId);
    Task<ApiResponse<List<WasteDto>>> GetWastesAsync(Guid branchId, DateOnly date);
}
