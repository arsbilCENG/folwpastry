using PastryFlow.Domain.Enums;

namespace PastryFlow.Domain.Entities;

public class CakeOption : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public CakeOptionType OptionType { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}
