using System;
using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class DayClosingCorrectionDto
{
    [Required]
    public Guid DayClosingDetailId { get; set; }
    
    [Required, Range(0, 99999)]
    public decimal CorrectedEndOfDayCount { get; set; }
    
    [Required, Range(0, 99999)]
    public decimal CorrectedCarryOverQuantity { get; set; }
    
    [Required, MinLength(10), MaxLength(500)]
    public string CorrectionReason { get; set; } = string.Empty;
}
