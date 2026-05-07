using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.CashTransactions;

public class CashTransactionDto
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateCashTransactionDto
{
    public Guid BranchId { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Today;
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class BranchCashSummaryDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    
    // Bugünkü açılış bakiyesi (dünkü kapanış nakitinden)
    public decimal OpeningCashBalance { get; set; }
    
    // Bugünkü hareketler
    public decimal TodayCashSales { get; set; }       // Nakit satış geliri
    public decimal TodayCashPurchases { get; set; }   // Nakit satın alım gideri
    public decimal TodayWithdrawals { get; set; }     // Admin çekimleri
    public decimal TodayDeposits { get; set; }        // Admin yatırımları
    
    // Hesaplanan beklenen kasa
    public decimal ExpectedCashBalance { get; set; }
    
    // Son gün kapanışından gelen gerçek sayım (varsa)
    public decimal? LastCountedCash { get; set; }
    
    public DateTime? LastUpdated { get; set; }
}
