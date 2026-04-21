using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class ResetPasswordDto
{
    [Required, MinLength(8), MaxLength(100)]
    public string NewPassword { get; set; } = string.Empty;
}
