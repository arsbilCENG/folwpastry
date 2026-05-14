namespace PastryFlow.Application.DTOs.Report;

// ─── Günlük Özet ───────────────────────────────────────────

public class DailySummaryReportDto
{
    public DateOnly Date { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsClosed { get; set; }

    public decimal ProductSalesRevenue { get; set; }
    public decimal CounterSalesRevenue { get; set; }
    public decimal TotalSalesRevenue { get; set; }

    public decimal TotalPurchaseExpense { get; set; }
    public decimal CashPurchaseExpense { get; set; }
    public decimal CardPurchaseExpense { get; set; }

    public decimal ExpectedCashAmount { get; set; }
    public decimal ActualCashAmount { get; set; }
    public decimal PosAmount { get; set; }
    public decimal CashDifference { get; set; }

    public int WasteItemCount { get; set; }
    public decimal TotalWasteQuantity { get; set; }

    public List<DailyProductSaleDto> ProductSales { get; set; } = new();
    public List<DailyWasteDto> Wastes { get; set; } = new();
}

public class DailyProductSaleDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal SoldQuantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal Revenue { get; set; }
    public bool IsCounter { get; set; }
}

public class DailyWasteDto
{
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public string WasteTypeLabel { get; set; } = string.Empty;
}

// ─── Dönem Raporu ──────────────────────────────────────────

public class PeriodSummaryReportDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public int ClosedDayCount { get; set; }

    public decimal TotalSalesRevenue { get; set; }
    public decimal TotalCounterRevenue { get; set; }
    public decimal TotalPurchaseExpense { get; set; }
    public decimal TotalCashDifference { get; set; }
    public decimal TotalWasteQuantity { get; set; }

    public List<PeriodDailyRowDto> DailyRows { get; set; } = new();
    public List<PeriodProductSummaryDto> ProductSummaries { get; set; } = new();
}

public class PeriodDailyRowDto
{
    public DateOnly Date { get; set; }
    public decimal ProductSalesRevenue { get; set; }
    public decimal CounterSalesRevenue { get; set; }
    public decimal TotalSalesRevenue { get; set; }
    public decimal PurchaseExpense { get; set; }
    public decimal CashDifference { get; set; }
}

public class PeriodProductSummaryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal TotalSoldQuantity { get; set; }
    public decimal TotalRevenue { get; set; }
    public bool IsCounter { get; set; }
}

// ─── Yönetim Paneli (Admin) ────────────────────────────────

public class ManagementReportDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public List<BranchComparisonDto> BranchComparisons { get; set; } = new();
    public List<BranchWalletSummaryDto> WalletBalances { get; set; } = new();
    public List<WalletMovementDto> WalletMovements { get; set; } = new();

    public decimal GrandTotalRevenue { get; set; }
    public decimal GrandTotalExpense { get; set; }
    public decimal GrandTotalCashBalance { get; set; }
    public decimal GrandTotalBankBalance { get; set; }
}

public class BranchComparisonDto
{
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalSalesRevenue { get; set; }
    public decimal TotalPurchaseExpense { get; set; }
    public decimal TotalCashDifference { get; set; }
    public decimal NetRevenue { get; set; }
    public int ClosedDayCount { get; set; }
}

public class BranchWalletSummaryDto
{
    public string BranchName { get; set; } = string.Empty;
    public decimal CashBalance { get; set; }
    public decimal BankBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

public class WalletMovementDto
{
    public DateTime TransactionDate { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string TransactionTypeLabel { get; set; } = string.Empty;
    public string WalletTypeLabel { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
}

// ─── Üretim Raporu ──────────────────────────────────────────

public class ProductionReportSalesBranchDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
}

public class ProductionReportRowDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    // Key: BranchId (string format for JSON serialization compatibility), Value: ApprovedQuantity
    public Dictionary<string, decimal> BranchQuantities { get; set; } = new();
    public decimal TotalQuantity { get; set; }
}

public class ProductionReportDto
{
    public DateOnly ReportDate { get; set; }        // Raporun tarihi (bugün)
    public DateOnly DemandDate { get; set; }        // Talep tarihi (dün)
    public string ProductionBranchName { get; set; } = string.Empty;
    public List<ProductionReportSalesBranchDto> SalesBranches { get; set; } = new();
    public List<ProductionReportRowDto> Rows { get; set; } = new();
    public int TotalProductCount { get; set; }      // Toplam ürün çeşidi
    public decimal TotalQuantity { get; set; }      // Toplam üretilecek adet
}
