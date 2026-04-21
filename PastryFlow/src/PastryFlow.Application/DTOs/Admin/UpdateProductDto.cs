using System;
using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class UpdateProductDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public Guid CategoryId { get; set; }
    
    public Guid? ProductionBranchId { get; set; }
    
    [Required]
    public string UnitType { get; set; } = "Adet";
    
    [Range(0, 99999.99)]
    public decimal? UnitPrice { get; set; }
    
    public bool IsRawMaterial { get; set; } = false;
    public int SortOrder { get; set; } = 0;
}
