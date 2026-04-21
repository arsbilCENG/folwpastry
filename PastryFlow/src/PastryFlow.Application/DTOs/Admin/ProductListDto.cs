using System;

namespace PastryFlow.Application.DTOs.Admin;

public class ProductListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public Guid? ProductionBranchId { get; set; }
    public string? ProductionBranchName { get; set; }
    public string UnitType { get; set; } = string.Empty;
    public decimal? UnitPrice { get; set; }
    public bool IsRawMaterial { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }
}
