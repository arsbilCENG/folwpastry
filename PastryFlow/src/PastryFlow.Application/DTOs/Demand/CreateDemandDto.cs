using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Demand;

public class CreateDemandDto
{
    public Guid SalesBranchId { get; set; }
    public Guid ProductionBranchId { get; set; }
    public string? Notes { get; set; }
    public List<CreateDemandItemDto> Items { get; set; } = new();
}
