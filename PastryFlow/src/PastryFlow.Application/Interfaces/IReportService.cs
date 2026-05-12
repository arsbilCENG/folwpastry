using PastryFlow.Application.DTOs.Report;

namespace PastryFlow.Application.Interfaces;

public interface IReportService
{
    Task<DailySummaryReportDto> GetDailySummaryAsync(
        Guid branchId, DateOnly date);

    Task<PeriodSummaryReportDto> GetPeriodSummaryAsync(
        Guid branchId, DateOnly startDate, DateOnly endDate);

    Task<ManagementReportDto> GetManagementReportAsync(
        DateOnly startDate, DateOnly endDate);
}
