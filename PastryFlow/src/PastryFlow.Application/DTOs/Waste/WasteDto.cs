using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Waste;

public class WasteDto
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public WasteType WasteType { get; set; }
    public string WasteTypeName => WasteType.ToString();
    public string? PhotoPath { get; set; }
    public string? Notes { get; set; }
    public DateOnly Date { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
