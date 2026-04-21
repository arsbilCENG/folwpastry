using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Reports;

public class DailySalesReportDto
{
    public DateTime Date { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<DailySalesItemDto> Items { get; set; } = new();
    public decimal TotalCalculatedSales { get; set; }
    public decimal TotalWaste { get; set; }
    public decimal? TotalSalesValue { get; set; }
}

public class DailySalesItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public decimal OpeningStock { get; set; }
    public decimal ReceivedFromDemand { get; set; }
    public decimal ReceivedFromTransfer { get; set; }
    public decimal SentByTransfer { get; set; }
    public decimal WasteQuantity { get; set; }
    public decimal EndOfDayCount { get; set; }
    public decimal CalculatedSales { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? SalesValue { get; set; }
}
