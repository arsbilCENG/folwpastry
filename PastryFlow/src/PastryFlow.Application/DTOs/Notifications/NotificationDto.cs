using System;

namespace PastryFlow.Application.DTOs.Notifications;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;       // NotificationType enum adı
    public string? SourceEntity { get; set; }
    public Guid? SourceEntityId { get; set; }
    public Guid? SourceBranchId { get; set; }
    public string? SourceBranchName { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TimeAgo { get; set; } = string.Empty;
}
