using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class CustomCakeOrder : BaseEntity
{
    public string OrderNumber { get; set; } = string.Empty;
    
    public Guid BranchId { get; set; }
    public Guid CreatedByUserId { get; set; }
    
    public Guid ProductionBranchId { get; set; }
    
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    
    public DateTime DeliveryDate { get; set; }
    public int ServingSize { get; set; }
    
    public Guid CakeTypeId { get; set; }
    public Guid InnerCreamId { get; set; }
    public Guid OuterCreamId { get; set; }
    
    public string Description { get; set; } = string.Empty;
    public string? ReferencePhotoUrl { get; set; }
    
    public decimal Price { get; set; }
    
    public CustomCakeOrderStatus Status { get; set; } = CustomCakeOrderStatus.Created;
    public string? StatusNote { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public Guid? StatusChangedByUserId { get; set; }
    
    public virtual Branch? Branch { get; set; }
    public virtual User? CreatedByUser { get; set; }
    public virtual Branch? ProductionBranch { get; set; }
    public virtual CakeOption? CakeType { get; set; }
    public virtual CakeOption? InnerCream { get; set; }
    public virtual CakeOption? OuterCream { get; set; }
    public virtual User? StatusChangedByUser { get; set; }
}
