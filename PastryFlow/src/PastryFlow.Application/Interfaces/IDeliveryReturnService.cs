using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.DeliveryReturns;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PastryFlow.Application.Interfaces;

public interface IDeliveryReturnService
{
    Task<ApiResponse<List<DeliveryReturnDto>>> GetReturnsAsync(Guid? branchId, DateTime? startDate, DateTime? endDate);
    Task<ApiResponse<List<DeliveryReturnDto>>> GetReturnsByDemandAsync(Guid demandId);
    Task<ApiResponse<bool>> AcknowledgeReturnAsync(Guid returnId, Guid userId);
}
