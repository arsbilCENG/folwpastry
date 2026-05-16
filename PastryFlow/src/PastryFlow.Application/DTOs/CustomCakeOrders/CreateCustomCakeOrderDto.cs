using System;
using System.ComponentModel.DataAnnotations;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class CreateCustomCakeOrderDto
{
    public string? CustomerName { get; set; }
    
    [MaxLength(20)]
    public string? CustomerPhone { get; set; }
    
    [Required]
    public DateTime DeliveryDate { get; set; }
    
    [Required]
    [Range(1, 500)]
    public int ServingSize { get; set; }
    
    [Required]
    public Guid CakeTypeId { get; set; }
    
    [Required]
    public Guid InnerCreamId { get; set; }
    
    [Required]
    public Guid OuterCreamId { get; set; }
    
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    public decimal? DepositAmount { get; set; }
    public PaymentMethod? DepositPaymentMethod { get; set; }
}
