using System;

namespace PastryFlow.Application.DTOs.Admin;

public class CategoryListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; }
}
