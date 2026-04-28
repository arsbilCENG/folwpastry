using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class DeliveryReturn : BaseEntity
{
    public Guid DemandId { get; set; }
    public Guid DemandItemId { get; set; }
    public Guid ProductId { get; set; }
    public Guid FromBranchId { get; set; }      // Tezgah (reddeden)
    public Guid ToBranchId { get; set; }        // Mutfak (iade edilen yer)
    
    public decimal Quantity { get; set; }        // İade miktarı
    public string Reason { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    
    public DeliveryReturnStatus Status { get; set; } = DeliveryReturnStatus.Created;
    
    // Navigation
    public virtual Demand? Demand { get; set; }
    public virtual DemandItem? DemandItem { get; set; }
    public virtual Product? Product { get; set; }
    public virtual Branch? FromBranch { get; set; }
    public virtual Branch? ToBranch { get; set; }
}
