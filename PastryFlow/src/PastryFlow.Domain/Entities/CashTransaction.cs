using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class CashTransaction : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }  // Cash veya BankTransfer
    public string Description { get; set; } = string.Empty;
    
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
}
