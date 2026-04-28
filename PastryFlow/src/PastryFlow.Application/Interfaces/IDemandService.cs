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
    Task<ApiResponse<DemandDto>> ReviewDemandAsync(Guid id, ReviewDemandDto dto);
    Task<ApiResponse<DemandDto>> ShipDemandAsync(Guid id, ShipDemandDto dto, Guid userId);
    Task<ApiResponse<DemandDto>> AcceptDeliveryAsync(Guid id, AcceptDeliveryDto dto, Guid userId);
    Task<ApiResponse<string>> UpdateRejectionPhotoAsync(Guid itemId, string photoUrl);
    Task<ApiResponse<DemandDto?>> GetLastDemandAsync(Guid salesBranchId, Guid productionBranchId);
    
    // Obsolete/Old flow methods
    [Obsolete("Use ShipDemandAsync")]
    Task<ApiResponse<DemandDto>> DeliverDemandAsync(Guid id, DeliverDemandDto dto);
    [Obsolete("Use AcceptDeliveryAsync")]
    Task<ApiResponse<DemandDto>> ReceiveDemandAsync(Guid id, ReceiveDemandDto dto);
}
