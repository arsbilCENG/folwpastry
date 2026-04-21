using System;

namespace PastryFlow.Domain.Entities;

public class DayClosingDetail : BaseEntity
{
    public Guid DayClosingId { get; set; }
    public Guid ProductId { get; set; }
    
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
    
    // Correction fields
    public decimal? OriginalEndOfDayCount { get; set; }
    public decimal? OriginalCarryOverQuantity { get; set; }
    public decimal? CorrectedEndOfDayCount { get; set; }
    public decimal? CorrectedCarryOverQuantity { get; set; }
    public string? CorrectionReason { get; set; }
    public DateTime? CorrectedAt { get; set; }
    public Guid? CorrectedByUserId { get; set; }

    // Navigation properties
    public virtual DayClosing DayClosing { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public virtual User? CorrectedByUser { get; set; }
}
