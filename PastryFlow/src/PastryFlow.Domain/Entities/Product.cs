using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid? ProductionBranchId { get; set; }
    public ProductType ProductType { get; set; }
    public UnitType Unit { get; set; }
    public decimal? UnitPrice { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Category Category { get; set; } = null!;
    public virtual Branch? ProductionBranch { get; set; }
}
