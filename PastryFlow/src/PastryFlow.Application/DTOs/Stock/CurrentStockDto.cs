using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Stock;

public class CurrentStockDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public UnitType Unit { get; set; }
    public string UnitName => Unit.ToString();
    
    public decimal OpeningStock { get; set; }
    public decimal ReceivedFromDemands { get; set; }
    public decimal IncomingTransfer { get; set; }
    public decimal OutgoingTransfer { get; set; }
    public decimal DayWaste { get; set; }
    
    // calculated: openingStock + receivedFromDemands + incomingTransfer - outgoingTransfer - dayWaste
    public decimal CurrentStock { get; set; }
}
