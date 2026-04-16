using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public Guid? BranchId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Branch? Branch { get; set; }
}
