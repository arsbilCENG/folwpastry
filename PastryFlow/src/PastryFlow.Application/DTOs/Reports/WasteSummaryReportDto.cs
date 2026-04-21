using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Reports;

public class WasteSummaryReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }
    public List<WasteSummaryItemDto> Items { get; set; } = new();
    public decimal TotalWasteQuantity { get; set; }
    public decimal? TotalEstimatedLoss { get; set; }
}

public class WasteSummaryItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public int WasteCount { get; set; }
    public decimal? EstimatedLoss { get; set; }
}
