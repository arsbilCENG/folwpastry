using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Notification;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;

    public NotificationService(IPastryFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateAsync(Guid? userId, Guid? branchId, string title, string message, string? relatedEntityType = null, Guid? relatedEntityId = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            BranchId = branchId,
            Title = title,
            Message = message,
            IsRead = false,
            RelatedEntityType = relatedEntityType,
            RelatedEntityId = relatedEntityId
        };
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<ApiResponse<List<NotificationDto>>> GetNotificationsAsync(Guid? userId, Guid? branchId, bool unreadOnly = false)
    {
        var query = _context.Notifications.AsQueryable();

        if (userId.HasValue)
            query = query.Where(n => n.UserId == userId.Value);

        if (branchId.HasValue)
            query = query.Where(n => n.BranchId == branchId.Value);

        if (unreadOnly)
            query = query.Where(n => !n.IsRead);

        var notifications = await query.OrderByDescending(n => n.CreatedAt).ToListAsync();
        return ApiResponse<List<NotificationDto>>.Ok(_mapper.Map<List<NotificationDto>>(notifications));
    }

    public async Task<ApiResponse<int>> GetUnreadCountAsync(Guid? userId, Guid? branchId)
    {
        var query = _context.Notifications.Where(n => !n.IsRead);

        if (userId.HasValue)
            query = query.Where(n => n.UserId == userId.Value);

        if (branchId.HasValue)
            query = query.Where(n => n.BranchId == branchId.Value);

        var count = await query.CountAsync();
        return ApiResponse<int>.Ok(count);
    }

    public async Task<ApiResponse<bool>> MarkAsReadAsync(Guid id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
            return ApiResponse<bool>.Fail("Bildirim bulunamadı.");

        notification.IsRead = true;
        await _context.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }
}
