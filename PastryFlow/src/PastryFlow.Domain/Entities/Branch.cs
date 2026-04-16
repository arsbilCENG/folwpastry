using System;
using System.Collections.Generic;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public City City { get; set; }
    public BranchType BranchType { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
