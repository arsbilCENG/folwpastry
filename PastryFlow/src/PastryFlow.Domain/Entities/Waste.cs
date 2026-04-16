using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Waste : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public WasteType WasteType { get; set; }
    public string? PhotoPath { get; set; }
    public string? Notes { get; set; }
    public DateOnly Date { get; set; }
    public Guid CreatedByUserId { get; set; }

    // Navigation properties
    public virtual Branch Branch { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
}
