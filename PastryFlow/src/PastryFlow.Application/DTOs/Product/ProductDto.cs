using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public Guid? ProductionBranchId { get; set; }
    public string? ProductionBranchName { get; set; }
    public ProductType ProductType { get; set; }
    public string ProductTypeName => ProductType.ToString();
    public UnitType Unit { get; set; }
    public string UnitName => Unit.ToString();
    public decimal? UnitPrice { get; set; }
    public bool IsActive { get; set; }
}
