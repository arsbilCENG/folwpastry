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
    Task<ApiResponse<List<DemandDto>>> GetDemandsAsync(Guid? branchId = null, Guid? productionBranchId = null, DemandStatus? status = null, DateOnly? date = null);
    Task<ApiResponse<DemandDto>> GetDemandByIdAsync(Guid id);
    Task<ApiResponse<DemandDto>> ReceiveDemandAsync(Guid id, ReceiveDemandDto dto);
    Task<ApiResponse<DemandDto>> ReviewDemandAsync(Guid id, ReviewDemandDto dto);
    Task<ApiResponse<DemandDto>> DeliverDemandAsync(Guid id, DeliverDemandDto dto);
    Task<ApiResponse<DemandDto?>> GetLastDemandAsync(Guid salesBranchId, Guid productionBranchId);
}
