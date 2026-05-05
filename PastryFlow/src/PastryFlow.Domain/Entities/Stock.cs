using System;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Domain.Entities;

public class Stock : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid ProductId { get; set; }
    public decimal CurrentQuantity { get; set; }

    // Navigation properties
    public virtual Branch Branch { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
