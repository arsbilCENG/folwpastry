using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class AdminWallet : BaseEntity
{
    public WalletType WalletType { get; set; }
    public decimal CurrentBalance { get; set; }
    public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
}
