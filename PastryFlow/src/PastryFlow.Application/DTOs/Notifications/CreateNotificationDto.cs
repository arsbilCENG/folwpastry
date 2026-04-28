using System;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.DTOs.Notifications;

public class CreateNotificationDto
{
    public Guid? UserId { get; set; }
    public Guid? BranchId { get; set; }
    public string? TargetRole { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public string? SourceEntity { get; set; }
    public Guid? SourceEntityId { get; set; }
    public Guid? SourceBranchId { get; set; }
    public string? SourceBranchName { get; set; }
}
