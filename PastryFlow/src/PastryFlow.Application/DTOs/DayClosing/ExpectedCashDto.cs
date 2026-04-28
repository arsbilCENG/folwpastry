using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.DayClosing;

public class ExpectedCashDto
{
    public decimal ExpectedAmount { get; set; }
    public int ProductsWithPrice { get; set; }
    public int ProductsWithoutPrice { get; set; }
    public List<ExpectedCashItemDto> Items { get; set; } = new();
}

public class ExpectedCashItemDto
{
    public string ProductName { get; set; } = string.Empty;
    public decimal CalculatedSales { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? SalesValue { get; set; }
}
