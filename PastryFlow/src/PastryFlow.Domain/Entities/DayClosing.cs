using System;
using System.Collections.Generic;

namespace PastryFlow.Domain.Entities;

public class DayClosing : BaseEntity
{
    public Guid BranchId { get; set; }
    public DateOnly Date { get; set; }
    public bool IsOpened { get; set; } = false;
    public DateTime? OpenedAt { get; set; }
    public Guid? OpenedByUserId { get; set; }
    public bool IsClosed { get; set; } = false;
    public DateTime? ClosedAt { get; set; }
    public Guid? ClosedByUserId { get; set; }

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

    // Navigation properties
    public virtual Branch Branch { get; set; } = null!;
    public virtual User? OpenedByUser { get; set; }
    public virtual User? ClosedByUser { get; set; }
    public virtual ICollection<DayClosingDetail> Details { get; set; } = new List<DayClosingDetail>();
}
