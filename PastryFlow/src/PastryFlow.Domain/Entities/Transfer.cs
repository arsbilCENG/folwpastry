using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Transfer : BaseEntity
{
    public string TransferNumber { get; set; } = string.Empty;
    public Guid FromBranchId { get; set; }
    public Guid ToBranchId { get; set; }
    public TransferStatus Status { get; set; } = TransferStatus.Pending;
    public string? Notes { get; set; }
    public Guid CreatedByUserId { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public Guid? DriverUserId { get; set; }
    public Guid? ReceivedByUserId { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? ReceivedAt { get; set; }

    // Navigation properties
    public virtual Branch FromBranch { get; set; } = null!;
    public virtual Branch ToBranch { get; set; } = null!;
    public virtual ICollection<TransferItem> Items { get; set; } = new List<TransferItem>();
}
