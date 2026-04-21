using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Reports;

public class BranchComparisonReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Metric { get; set; } = string.Empty;
    public List<BranchComparisonItemDto> Items { get; set; } = new();
}

public class BranchComparisonItemDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<DailyMetricDto> DailyData { get; set; } = new();
    public decimal Total { get; set; }
}

public class DailyMetricDto
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}
