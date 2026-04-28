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

    // -- Shipment (Mutfak gönderirken) --
    public decimal? SentQuantity { get; set; }
    public DateTime? SentAt { get; set; }

    // -- Acceptance (Tezgah teslim alırken) --
    public decimal? AcceptedQuantity { get; set; }
    public decimal? RejectedQuantity { get; set; }
    public string? DeliveryRejectionReason { get; set; }
    public string? RejectionPhotoUrl { get; set; }
    public DateTime? AcceptedAt { get; set; }

    // Navigation properties
    public virtual Demand Demand { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
