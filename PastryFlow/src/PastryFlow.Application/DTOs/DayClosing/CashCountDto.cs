using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.DayClosing;

public class CashCountDto
{
    [Required]
    [Range(0, 9999999.99)]
    public decimal CashAmount { get; set; }
    
    [Required]
    [Range(0, 9999999.99)]
    public decimal PosAmount { get; set; }
    
    [MaxLength(500)]
    public string? DifferenceNote { get; set; }
}
