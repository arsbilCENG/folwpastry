using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Transfer : BaseEntity
{
    public string TransferNumber { get; set; } = string.Empty;
    // Format: TRF-2026-0001

    public Guid SenderBranchId { get; set; }
    public Branch SenderBranch { get; set; } = null!;

    public Guid ReceiverBranchId { get; set; }
    public Branch ReceiverBranch { get; set; } = null!;

    public TransferStatus Status { get; set; } = TransferStatus.Shipped;

    public DateTime ShippedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }

    public Guid CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;

    public Guid? ReceivedByUserId { get; set; }
    public User? ReceivedBy { get; set; }

    public Guid? CancelledByUserId { get; set; }
    public User? CancelledBy { get; set; }

    public ICollection<TransferItem> Items { get; set; } = new List<TransferItem>();
}

public class TransferItem : BaseEntity
{
    public Guid TransferId { get; set; }
    public Transfer Transfer { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public decimal Quantity { get; set; }
}
