using System;

namespace PastryFlow.Application.DTOs.Demand;

public class ReviewDemandItemDto
{
    public Guid DemandItemId { get; set; }
    public string Status { get; set; } = string.Empty; // "Approved" | "Rejected"
    public decimal? ApprovedQuantity { get; set; }
    public string? RejectionReason { get; set; }
}
