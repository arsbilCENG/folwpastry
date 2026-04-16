using System;
using System.Collections.Generic;
using PastryFlow.Application.DTOs.Product;

namespace PastryFlow.Application.DTOs.Category;

public class CategoryWithProductsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}
