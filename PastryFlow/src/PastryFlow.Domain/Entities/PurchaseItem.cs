namespace PastryFlow.Domain.Entities;

public class PurchaseItem : BaseEntity
{
    public Guid PurchaseId { get; set; }
    public Purchase Purchase { get; set; } = null!;
    
    public Guid? ProductId { get; set; }       // Nullable — serbest metin girişi için
    public Product? Product { get; set; }
    
    public string ItemName { get; set; } = string.Empty;  // Her zaman dolu
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = "Adet";
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }    // Quantity × UnitPrice
    
    // Stok etkisi: ProductId != null && Product.TrackingType == Purchased
    // Service katmanında hesaplanır, DB'de saklanır (performans için)
    public bool AffectsStock { get; set; } = false;
}
