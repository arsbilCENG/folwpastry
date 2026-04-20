using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Notification;

namespace PastryFlow.Application.Interfaces;

public interface INotificationService
{
    Task CreateAsync(Guid? userId, Guid? branchId, string title, string message, string? relatedEntityType = null, Guid? relatedEntityId = null);
    Task<ApiResponse<List<NotificationDto>>> GetNotificationsAsync(Guid? userId, Guid? branchId, bool unreadOnly = false);
    Task<ApiResponse<int>> GetUnreadCountAsync(Guid? userId, Guid? branchId);
    Task<ApiResponse<bool>> MarkAsReadAsync(Guid id);
}
