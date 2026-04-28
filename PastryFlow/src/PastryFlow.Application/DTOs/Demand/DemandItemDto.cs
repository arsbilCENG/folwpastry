using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Demand;

public class DemandItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public UnitType Unit { get; set; }
    public string UnitName => Unit.ToString();
    public decimal RequestedQuantity { get; set; }
    public decimal? ApprovedQuantity { get; set; }
    public DemandItemStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public string? RejectionReason { get; set; }
    
    public decimal? SentQuantity { get; set; }
    public DateTime? SentAt { get; set; }
    public decimal? AcceptedQuantity { get; set; }
    public decimal? RejectedQuantity { get; set; }
    public string? DeliveryRejectionReason { get; set; }
    public string? RejectionPhotoUrl { get; set; }
    public DateTime? AcceptedAt { get; set; }
}
