using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Purchase : BaseEntity
{
    public string PurchaseNumber { get; set; } = string.Empty; // PUR-2026-0001
    public Guid? BranchId { get; set; }
    public Branch? Branch { get; set; }
    
    public DateTime PurchaseDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal TotalAmount { get; set; }  // Sum of items
    
    public string? ReceiptPhotoUrl { get; set; }  // Zorunlu ama ayrı endpoint ile
    public string? Notes { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    
    public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
}
