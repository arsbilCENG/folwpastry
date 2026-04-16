using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Demand;

public class DemandDto
{
    public Guid Id { get; set; }
    public string DemandNumber { get; set; } = string.Empty;
    public Guid SalesBranchId { get; set; }
    public string SalesBranchName { get; set; } = string.Empty;
    public Guid ProductionBranchId { get; set; }
    public string ProductionBranchName { get; set; } = string.Empty;
    public DemandStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? ReceivedAt { get; set; }
    public Guid? ReceivedByUserId { get; set; }
    
    public List<DemandItemDto> Items { get; set; } = new();
}
