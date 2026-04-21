using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class UpdateBranchDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required, MaxLength(100)]
    public string City { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
}
