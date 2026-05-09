using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Wallet;

public class BranchWalletDto
{
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public decimal CashBalance { get; set; }
    public decimal BankBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

public class AdminWalletDto
{
    public decimal CashBalance { get; set; }
    public decimal BankBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

public class WalletSummaryDto
{
    public List<BranchWalletDto> Branches { get; set; } = new();
    public AdminWalletDto Admin { get; set; } = new();
    public decimal GrandTotalCash { get; set; }
    public decimal GrandTotalBank { get; set; }
    public decimal GrandTotal { get; set; }
}

public class WalletTransactionDto
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionTypeLabel { get; set; } = string.Empty;
    public string WalletTypeLabel { get; set; } = string.Empty;
    public string? SourceLabel { get; set; }
    public string? TargetLabel { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
}

public class SetInitialBalanceRequest
{
    public Guid? BranchId { get; set; }   // null = AdminWallet
    public decimal CashBalance { get; set; }
    public decimal BankBalance { get; set; }
}

public class TransferRequest
{
    public Guid BranchId { get; set; }
    public WalletType WalletType { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}

public class ManualAdjustmentRequest
{
    public Guid? BranchId { get; set; }   // null = AdminWallet
    public WalletType WalletType { get; set; }
    public decimal Amount { get; set; }   // pozitif = ekle, negatif = çıkar
    public string Description { get; set; } = string.Empty;
}
