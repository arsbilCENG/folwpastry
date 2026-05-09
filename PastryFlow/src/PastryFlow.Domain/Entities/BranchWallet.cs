using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class BranchWallet : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public WalletType WalletType { get; set; }
    public decimal CurrentBalance { get; set; }
    public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
}
