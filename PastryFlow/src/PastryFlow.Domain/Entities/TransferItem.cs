using System;

namespace PastryFlow.Domain.Entities;

public class TransferItem : BaseEntity
{
    public Guid TransferId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal? ReceivedQuantity { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public virtual Transfer Transfer { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
