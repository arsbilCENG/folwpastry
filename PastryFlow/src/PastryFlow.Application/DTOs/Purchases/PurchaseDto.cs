using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Purchases;

public class PurchaseDto
{
    public Guid Id { get; set; }
    public string PurchaseNumber { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal TotalAmount { get; set; }
    public string? ReceiptPhotoUrl { get; set; }
    public string? Notes { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<PurchaseItemDto> Items { get; set; } = new();
}

public class PurchaseItemDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool AffectsStock { get; set; }
}

public class CreatePurchaseDto
{
    public DateTime PurchaseDate { get; set; } = DateTime.Today;
    public PaymentMethod PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public List<CreatePurchaseItemDto> Items { get; set; } = new();
}

public class CreatePurchaseItemDto
{
    public Guid? ProductId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = "Adet";
    public decimal UnitPrice { get; set; }
}
