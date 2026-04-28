using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Hubs;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(
        IPastryFlowDbContext context,
        IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task CreateAndSendAsync(CreateNotificationDto dto)
    {
        // 1. DB'ye kaydet
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            BranchId = dto.BranchId,
            TargetRole = dto.TargetRole,
            Title = dto.Title,
            Message = dto.Message,
            Type = dto.Type,
            SourceEntity = dto.SourceEntity,
            SourceEntityId = dto.SourceEntityId,
            SourceBranchId = dto.SourceBranchId,
            SourceBranchName = dto.SourceBranchName,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        // 2. SignalR ile gönder
        var notificationDto = MapToDto(notification);
        
        // Spesifik kullanıcıya
        if (dto.UserId.HasValue)
        {
            await _hubContext.Clients
                .Group($"user_{dto.UserId.Value}")
                .SendAsync("ReceiveNotification", notificationDto);
        }
        
        // Şubeye
        if (dto.BranchId.HasValue)
        {
            await _hubContext.Clients
                .Group($"branch_{dto.BranchId.Value}")
                .SendAsync("ReceiveNotification", notificationDto);
        }
        
        // Role bazlı (Admin'e)
        if (!string.IsNullOrEmpty(dto.TargetRole))
        {
            await _hubContext.Clients
                .Group($"role_{dto.TargetRole}")
                .SendAsync("ReceiveNotification", notificationDto);
        }
    }

    public async Task<NotificationListDto> GetUserNotificationsAsync(
        Guid userId, Guid? branchId, string role,
        int pageNumber = 1, int pageSize = 20, bool? isRead = null)
    {
        var query = _context.Notifications.AsQueryable();

        // Kullanıcının görebileceği bildirimler:
        // 1. Direkt kendisine gönderilen (UserId == userId)
        // 2. Şubesine gönderilen (BranchId == branchId)
        // 3. Rolüne gönderilen (TargetRole == role)
        query = query.Where(n => 
            (n.UserId == userId) ||
            (branchId.HasValue && n.BranchId == branchId.Value) ||
            (n.TargetRole == role));

        if (isRead.HasValue)
        {
            query = query.Where(n => n.IsRead == isRead.Value);
        }

        var totalCount = await query.CountAsync();
        var unreadCount = await query.CountAsync(n => !n.IsRead);

        var items = await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new NotificationListDto
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            UnreadCount = unreadCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<int> GetUnreadCountAsync(Guid userId, Guid? branchId, string role)
    {
        return await _context.Notifications
            .Where(n => 
                (n.UserId == userId) ||
                (branchId.HasValue && n.BranchId == branchId.Value) ||
                (n.TargetRole == role))
            .CountAsync(n => !n.IsRead);
    }

    public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId);
        
        if (notification == null) return;
        
        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task MarkAllAsReadAsync(Guid userId, Guid? branchId, string role)
    {
        var notifications = await _context.Notifications
            .Where(n => !n.IsRead &&
                ((n.UserId == userId) ||
                 (branchId.HasValue && n.BranchId == branchId.Value) ||
                 (n.TargetRole == role)))
            .ToListAsync();

        foreach (var n in notifications)
        {
            n.IsRead = true;
            n.ReadAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }

    private NotificationDto MapToDto(Notification n)
    {
        return new NotificationDto
        {
            Id = n.Id,
            Title = n.Title,
            Message = n.Message,
            Type = n.Type.ToString(),
            SourceEntity = n.SourceEntity,
            SourceEntityId = n.SourceEntityId,
            SourceBranchId = n.SourceBranchId,
            SourceBranchName = n.SourceBranchName,
            IsRead = n.IsRead,
            ReadAt = n.ReadAt,
            CreatedAt = n.CreatedAt,
            TimeAgo = TimeAgoHelper.Calculate(n.CreatedAt)
        };
    }
}
