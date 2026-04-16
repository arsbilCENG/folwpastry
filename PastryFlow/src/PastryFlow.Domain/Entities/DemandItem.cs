using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class DemandItem : BaseEntity
{
    public Guid DemandId { get; set; }
    public Guid ProductId { get; set; }
    public decimal RequestedQuantity { get; set; }
    public decimal? ApprovedQuantity { get; set; }
    public DemandItemStatus Status { get; set; } = DemandItemStatus.Pending;
    public string? RejectionReason { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public DateTime? ReviewedAt { get; set; }

    // Navigation properties
    public virtual Demand Demand { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
