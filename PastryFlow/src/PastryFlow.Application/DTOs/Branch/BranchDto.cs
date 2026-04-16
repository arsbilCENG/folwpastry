using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Branch;

public class BranchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public City City { get; set; }
    public string CityName => City.ToString();
    public BranchType BranchType { get; set; }
    public string BranchTypeName => BranchType.ToString();
    public bool IsActive { get; set; }
}
