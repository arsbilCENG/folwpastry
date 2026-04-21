using System;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;

namespace PastryFlow.Application.Interfaces;

public interface IAdminDashboardService
{
    Task<ApiResponse<AdminDashboardDto>> GetDashboardSummaryAsync(DateTime? date = null);
}
