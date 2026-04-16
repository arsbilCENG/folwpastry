using System;

namespace PastryFlow.Application.DTOs.Demand;

public class CreateDemandItemDto
{
    public Guid ProductId { get; set; }
    public decimal RequestedQuantity { get; set; }
}
