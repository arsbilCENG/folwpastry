using System;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class CustomCakeOrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public Guid ProductionBranchId { get; set; }
    public string ProductionBranchName { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }
    public string DeliveryDate { get; set; } = string.Empty;
    public int ServingSize { get; set; }
    public Guid CakeTypeId { get; set; }
    public string CakeTypeName { get; set; } = string.Empty;
    public Guid InnerCreamId { get; set; }
    public string InnerCreamName { get; set; } = string.Empty;
    public Guid OuterCreamId { get; set; }
    public string OuterCreamName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ReferencePhotoUrl { get; set; }
    public decimal Price { get; set; }
    
    public decimal? DepositAmount { get; set; }
    public string? DepositPaymentMethod { get; set; }
    public DateTime? DepositPaidAt { get; set; }
    public string? DepositCollectedByUserName { get; set; }
    
    public decimal? FinalPaymentAmount { get; set; }
    public string? FinalPaymentMethod { get; set; }
    public DateTime? FinalPaymentPaidAt { get; set; }
    public string? FinalPaymentCollectedByUserName { get; set; }
    
    public decimal RemainingAmount { get; set; }

    public string Status { get; set; } = string.Empty;
    public string StatusText { get; set; } = string.Empty;
    public string? StatusNote { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
