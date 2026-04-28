using System.ComponentModel.DataAnnotations;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class CreateCakeOptionDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public CakeOptionType OptionType { get; set; }
    
    public int SortOrder { get; set; } = 0;
}
