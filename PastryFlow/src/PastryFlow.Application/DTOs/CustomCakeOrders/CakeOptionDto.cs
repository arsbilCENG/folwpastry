using System;

namespace PastryFlow.Application.DTOs.CustomCakeOrders;

public class CakeOptionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OptionType { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
