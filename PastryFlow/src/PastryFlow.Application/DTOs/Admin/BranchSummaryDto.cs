using System;

namespace PastryFlow.Application.DTOs.Admin;

public class BranchSummaryDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string BranchType { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int PendingDemandCount { get; set; }
    public int ApprovedDemandCount { get; set; }
    public int TotalProductsInStock { get; set; }
    public decimal TotalWasteQuantity { get; set; }
    public bool IsDayOpened { get; set; }
    public bool IsDayClosed { get; set; }
}
