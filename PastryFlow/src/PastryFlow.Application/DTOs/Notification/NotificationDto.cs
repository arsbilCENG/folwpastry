using System;

namespace PastryFlow.Application.DTOs.Notification;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? BranchId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string? RelatedEntityType { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public DateTime CreatedAt { get; set; }
}
