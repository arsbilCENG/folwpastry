using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.DayClosing;

public class DayClosingSummaryDto
{
    public string BranchName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public bool IsClosed { get; set; }
    
    public List<DailySummaryItemDto> Items { get; set; } = new();
    public DayClosingTotals Totals { get; set; } = new();
}

public class DailySummaryItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal OpeningStock { get; set; }
    public decimal ReceivedFromDemands { get; set; }
    public decimal IncomingTransfer { get; set; }
    public decimal OutgoingTransfer { get; set; }
    public decimal DayWaste { get; set; }
    public decimal EndOfDayCount { get; set; }
    public decimal CarryOver { get; set; }
    public decimal EndOfDayWaste { get; set; }
    public decimal CalculatedSales { get; set; }
}

public class DayClosingTotals
{
    public decimal TotalSales { get; set; }
    public decimal TotalWaste { get; set; }
    public decimal TotalCarryOver { get; set; }
}
