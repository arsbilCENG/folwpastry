using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Demand;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface IDemandService
{
    Task<ApiResponse<DemandDto>> CreateDemandAsync(Guid userId, CreateDemandDto dto);
    Task<ApiResponse<List<DemandDto>>> GetDemandsAsync(Guid? branchId = null, DemandStatus? status = null, DateOnly? date = null);
    Task<ApiResponse<DemandDto>> GetDemandByIdAsync(Guid id);
    Task<ApiResponse<DemandDto>> ReceiveDemandAsync(Guid id, ReceiveDemandDto dto);
}
