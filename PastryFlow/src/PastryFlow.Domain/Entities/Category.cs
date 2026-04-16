using System;
using System.Collections.Generic;

namespace PastryFlow.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
