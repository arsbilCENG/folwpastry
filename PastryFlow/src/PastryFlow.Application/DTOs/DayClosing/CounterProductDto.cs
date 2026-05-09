using System;

namespace PastryFlow.Application.DTOs.DayClosing;

/// <summary>
/// GET /day-closing/summary response'unda dönen Counter ürün bilgisi.
/// Kullanıcı bu listeden gün içinde kaç adet sattığını girer.
/// </summary>
public class CounterProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal? UnitPrice { get; set; }
    public string Unit { get; set; } = string.Empty;
}

/// <summary>
/// Gün kapatma isteğinde gönderilen Counter ürün satış kaydı.
/// </summary>
public class DayClosingCounterItemDto
{
    public Guid ProductId { get; set; }
    public decimal CounterSoldQuantity { get; set; }
}
