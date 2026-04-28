using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Demand;

public class AcceptDeliveryDto
{
    [Required]
    public List<AcceptDeliveryItemDto> Items { get; set; } = new();
}

public class AcceptDeliveryItemDto
{
    [Required]
    public Guid DemandItemId { get; set; }
    
    [Required, Range(0, 99999)]
    public decimal AcceptedQuantity { get; set; }
    
    public string? RejectionReason { get; set; }
}
