using System;
using System.Collections.Generic;

namespace PastryFlow.Application.DTOs.Reports;

public class DailyReportDto
{
    public DateTime Date { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<DailySalesItemDto> Items { get; set; } = new();
    public decimal TotalCalculatedSales { get; set; }
    public decimal TotalWaste { get; set; }
    public decimal? TotalSalesValue { get; set; }

    // Gelir tarafı
    public decimal CounterSalesRevenue { get; set; }      // Counter ürün satış geliri

    // Gider tarafı
    public decimal TotalPurchaseExpense { get; set; }     // Toplam satın alım gideri
    public decimal CashPurchaseExpense { get; set; }      // Nakit satın alım gideri
    public decimal CardPurchaseExpense { get; set; }      // Kartlı satın alım gideri

    // Kasa hareketleri
    public decimal TotalCashDeposits { get; set; }        // Admin yatırım toplamı
    public decimal TotalCashWithdrawals { get; set; }     // Admin çekim toplamı
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

public class PurchaseReportItemDto
{
    public Guid PurchaseId { get; set; }
    public string PurchaseNumber { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string PaymentMethodLabel { get; set; } = string.Empty; // "Nakit" / "Kredi Kartı"
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public List<PurchaseReportItemDetailDto> Items { get; set; } = new();
}

public class PurchaseReportItemDetailDto
{
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class PurchaseReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? BranchName { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal CashExpense { get; set; }
    public decimal CardExpense { get; set; }
    public List<PurchaseReportItemDto> Purchases { get; set; } = new();
}

public class CashTransactionReportItemDto
{
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string TransactionTypeLabel { get; set; } = string.Empty; // "Para Çekme" / "Para Yatırma"
    public string MethodLabel { get; set; } = string.Empty;          // "Nakit" / "Banka"
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
}

public class CashTransactionReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? BranchName { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public decimal NetFlow { get; set; } // Deposits - Withdrawals
    public List<CashTransactionReportItemDto> Transactions { get; set; } = new();
}

public class BranchComparisonReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Metric { get; set; } = string.Empty;
    public List<BranchComparisonItemDto> Items { get; set; } = new();
}

public class BranchComparisonItemDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<DailyMetricDto> DailyData { get; set; } = new();
    public decimal Total { get; set; }
}

public class DailyMetricDto
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}

public class WasteSummaryReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; }
    public List<WasteSummaryItemDto> Items { get; set; } = new();
    public decimal TotalWasteQuantity { get; set; }
    public decimal? TotalEstimatedLoss { get; set; }
}

public class WasteSummaryItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public int WasteCount { get; set; }
    public decimal? EstimatedLoss { get; set; }
}

public class DemandSummaryReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<DemandSummaryItemDto> Items { get; set; } = new();
    public int TotalDemands { get; set; }
    public int TotalApproved { get; set; }
    public int TotalRejected { get; set; }
    public decimal ApprovalRate { get; set; }
}

public class DemandSummaryItemDto
{
    public DateTime Date { get; set; }
    public Guid FromBranchId { get; set; }
    public string FromBranchName { get; set; } = string.Empty;
    public Guid ToBranchId { get; set; }
    public string ToBranchName { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public int ApprovedItems { get; set; }
    public int RejectedItems { get; set; }
    public string Status { get; set; } = string.Empty;
}
