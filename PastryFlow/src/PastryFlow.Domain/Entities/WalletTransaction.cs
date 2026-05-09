using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class WalletTransaction : BaseEntity
{
    public DateTime TransactionDate { get; set; }
    public WalletTransactionType TransactionType { get; set; }
    public WalletType WalletType { get; set; }

    // Kaynak
    public WalletPartyType SourceType { get; set; }
    public Guid? SourceBranchId { get; set; }
    public Branch? SourceBranch { get; set; }
    public Guid? SourceBranchWalletId { get; set; }
    public BranchWallet? SourceBranchWallet { get; set; }
    public Guid? SourceAdminWalletId { get; set; }
    public AdminWallet? SourceAdminWallet { get; set; }

    // Hedef
    public WalletPartyType TargetType { get; set; }
    public Guid? TargetBranchId { get; set; }
    public Branch? TargetBranch { get; set; }
    public Guid? TargetBranchWalletId { get; set; }
    public BranchWallet? TargetBranchWallet { get; set; }
    public Guid? TargetAdminWalletId { get; set; }
    public AdminWallet? TargetAdminWallet { get; set; }

    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Guid CreatedByUserId { get; set; }
    public User CreatedBy { get; set; } = null!;
}
