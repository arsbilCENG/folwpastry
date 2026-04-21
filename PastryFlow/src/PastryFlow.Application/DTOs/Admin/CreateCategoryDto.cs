using System.ComponentModel.DataAnnotations;

namespace PastryFlow.Application.DTOs.Admin;

public class CreateCategoryDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; } = 0;
}
