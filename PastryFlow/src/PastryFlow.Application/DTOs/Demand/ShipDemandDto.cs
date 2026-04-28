using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Demand;

public class ShipDemandDto
{
    [Required]
    public List<ShipDemandItemDto> Items { get; set; } = new();
}

public class ShipDemandItemDto
{
    [Required]
    public Guid DemandItemId { get; set; }
    
    [Required, Range(0, 99999)]
    public decimal SentQuantity { get; set; }
}
