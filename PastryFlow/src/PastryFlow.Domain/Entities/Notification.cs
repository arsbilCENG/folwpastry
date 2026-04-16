using System;

namespace PastryFlow.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid? BranchId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public string? RelatedEntityType { get; set; }
    public Guid? RelatedEntityId { get; set; }
}
