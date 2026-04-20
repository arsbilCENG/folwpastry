using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Waste;

public class CreateWasteDto
{
    public Guid BranchId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string? Notes { get; set; }
    public DateOnly Date { get; set; }
    public string? PhotoPath { get; set; }
}
