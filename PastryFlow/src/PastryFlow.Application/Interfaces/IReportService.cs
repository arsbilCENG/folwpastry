using System;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Reports;

namespace PastryFlow.Application.Interfaces;

public interface IReportService
{
    Task<ApiResponse<DailySalesReportDto>> GetDailySalesReportAsync(DateOnly date, Guid? branchId);
    Task<ApiResponse<WasteSummaryReportDto>> GetWasteSummaryReportAsync(DateOnly startDate, DateOnly endDate, Guid? branchId, Guid? categoryId);
    Task<ApiResponse<DemandSummaryReportDto>> GetDemandSummaryReportAsync(DateOnly startDate, DateOnly endDate, Guid? branchId);
    Task<ApiResponse<BranchComparisonReportDto>> GetBranchComparisonReportAsync(DateOnly startDate, DateOnly endDate, string metric);
}
