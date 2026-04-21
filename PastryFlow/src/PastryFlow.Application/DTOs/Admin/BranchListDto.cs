using System;

namespace PastryFlow.Application.DTOs.Admin;

public class BranchListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string BranchType { get; set; } = string.Empty;
    public Guid? PairedBranchId { get; set; }
    public string? PairedBranchName { get; set; }
    public bool IsActive { get; set; }
    public int UserCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
