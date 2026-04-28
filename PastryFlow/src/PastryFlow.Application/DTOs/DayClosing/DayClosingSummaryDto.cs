using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.DayClosing;

public class DayClosingSummaryDto
{
    public Guid? Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public bool IsClosed { get; set; }
    
    // Kasa Sayımı
    public decimal? ExpectedCashAmount { get; set; }
    public decimal? CashAmount { get; set; }
    public decimal? PosAmount { get; set; }
    public decimal? TotalCounted { get; set; }
    public decimal? CashDifference { get; set; }
    public string? DifferenceNote { get; set; }

    // Fotoğraflar
    public string? ReceiptPhotoUrl { get; set; }
    public string? CounterPhotoUrl { get; set; }
    
    public List<DailySummaryItemDto> Items { get; set; } = new();
    public DayClosingTotals Totals { get; set; } = new();
}

public class DailySummaryItemDto
{
    public Guid Id { get; set; }
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
    
    // Correction Info
    public bool IsCorrected { get; set; }
    public decimal? OriginalEndOfDayCount { get; set; }
    public decimal? OriginalCarryOverQuantity { get; set; }
    public string? LastCorrectionReason { get; set; }
}

public class DayClosingTotals
{
    public decimal TotalSales { get; set; }
    public decimal TotalWaste { get; set; }
    public decimal TotalCarryOver { get; set; }
}
