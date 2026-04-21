using System;
using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class UpdateUserDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Role { get; set; } = string.Empty;
    
    public Guid? BranchId { get; set; }
    public bool IsActive { get; set; } = true;
}
