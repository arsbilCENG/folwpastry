using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Stock;

namespace PastryFlow.Application.Interfaces;

public interface IStockService
{
    Task<ApiResponse<List<CurrentStockDto>>> GetCurrentStockAsync(Guid branchId, DateOnly date);
}
