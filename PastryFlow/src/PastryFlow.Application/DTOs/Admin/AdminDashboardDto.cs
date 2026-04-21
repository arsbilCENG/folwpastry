using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Admin;

public class AdminDashboardDto
{
    public DateTime Date { get; set; }
    public List<BranchSummaryDto> BranchSummaries { get; set; } = new();
    public int TotalPendingDemands { get; set; }
    public int TotalApprovedDemands { get; set; }
    public int TotalRejectedDemands { get; set; }
    public decimal TotalWasteToday { get; set; }
    public int BranchesOpenToday { get; set; }
    public int BranchesClosedToday { get; set; }
}
