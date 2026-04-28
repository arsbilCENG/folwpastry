using System;

namespace PastryFlow.Application.DTOs.DeliveryReturns;

public class DeliveryReturnDto
{
    public Guid Id { get; set; }
    public Guid DemandId { get; set; }
    public Guid DemandItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public Guid FromBranchId { get; set; }
    public string FromBranchName { get; set; } = string.Empty;
    public Guid ToBranchId { get; set; }
    public string ToBranchName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
