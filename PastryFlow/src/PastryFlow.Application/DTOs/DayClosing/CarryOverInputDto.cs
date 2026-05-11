using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.DayClosing;

public class CarryOverInputDto
{
    public Guid BranchId { get; set; }
    public string Date { get; set; } = string.Empty;
    public List<CarryOverItemDto> Items { get; set; } = new();
}

public class CarryOverItemDto
{
    public Guid ProductId { get; set; }
    public decimal CarryOverQuantity { get; set; }
}
