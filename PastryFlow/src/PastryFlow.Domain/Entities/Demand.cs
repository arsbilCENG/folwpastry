using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Demand : BaseEntity
{
    public string DemandNumber { get; set; } = string.Empty;
    public Guid SalesBranchId { get; set; }
    public Guid ProductionBranchId { get; set; }
    public DemandStatus Status { get; set; } = DemandStatus.Pending;
    public string? Notes { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public Guid? DriverUserId { get; set; }
    public Guid? ReceivedByUserId { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? ReceivedAt { get; set; }

    // Navigation properties
    public virtual Branch SalesBranch { get; set; } = null!;
    public virtual Branch ProductionBranch { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
    public virtual ICollection<DemandItem> Items { get; set; } = new List<DemandItem>();
}
