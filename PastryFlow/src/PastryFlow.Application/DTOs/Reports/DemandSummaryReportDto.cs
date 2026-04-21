using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Reports;

public class DemandSummaryReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DemandSummaryItemDto> Items { get; set; } = new();
    public int TotalDemands { get; set; }
    public int TotalApproved { get; set; }
    public int TotalRejected { get; set; }
    public decimal ApprovalRate { get; set; }
}

public class DemandSummaryItemDto
{
    public DateTime Date { get; set; }
    public Guid FromBranchId { get; set; }
    public string FromBranchName { get; set; } = string.Empty;
    public Guid ToBranchId { get; set; }
    public string ToBranchName { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public int ApprovedItems { get; set; }
    public int RejectedItems { get; set; }
    public string Status { get; set; } = string.Empty;
}
