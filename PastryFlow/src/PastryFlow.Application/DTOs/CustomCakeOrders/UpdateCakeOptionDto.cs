using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class UpdateCakeOptionDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
