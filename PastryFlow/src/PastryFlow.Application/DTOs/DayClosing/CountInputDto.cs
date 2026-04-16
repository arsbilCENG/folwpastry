using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.DayClosing;

public class CountInputDto
{
    public Guid BranchId { get; set; }
    public DateOnly Date { get; set; }
    public List<CountItemDto> Items { get; set; } = new();
}

public class CountItemDto
{
    public Guid ProductId { get; set; }
    public decimal EndOfDayCount { get; set; }
}
