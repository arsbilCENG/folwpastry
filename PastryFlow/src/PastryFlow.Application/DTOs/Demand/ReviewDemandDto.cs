using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Demand;

public class ReviewDemandDto
{
    public Guid ReviewedByUserId { get; set; }
    public List<ReviewDemandItemDto> Items { get; set; } = new();
}
