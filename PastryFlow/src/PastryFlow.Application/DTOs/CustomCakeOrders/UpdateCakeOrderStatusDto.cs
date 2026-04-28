using System.ComponentModel.DataAnnotations;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class UpdateCakeOrderStatusDto
{
    [Required]
    public CustomCakeOrderStatus NewStatus { get; set; }
    
    [MaxLength(500)]
    public string? StatusNote { get; set; }
}
