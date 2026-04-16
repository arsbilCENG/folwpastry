using System;

namespace PastryFlow.Domain.Entities;

public class DailyStockSummary : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid ProductId { get; set; }
    public DateOnly Date { get; set; }
    
    public decimal OpeningStock { get; set; } = 0;
    public decimal ReceivedFromDemands { get; set; } = 0;
    public decimal IncomingTransferQuantity { get; set; } = 0;
    public decimal OutgoingTransferQuantity { get; set; } = 0;
    public decimal DayWasteQuantity { get; set; } = 0;
    public decimal EndOfDayCount { get; set; } = 0;
    public decimal CarryOverQuantity { get; set; } = 0;
    
    // EndOfDayCount - CarryOverQuantity
    public decimal EndOfDayWaste { get; set; } = 0;
    
    // (Opening + ReceivedFromDemands + IncomingTransfer) - (OutgoingTransfer + EndOfDayCount + DayWaste)
    public decimal CalculatedSales { get; set; } = 0;
    
    public bool IsClosed { get; set; } = false;
    public Guid? ClosedByUserId { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Navigation properties
    public virtual Branch Branch { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
