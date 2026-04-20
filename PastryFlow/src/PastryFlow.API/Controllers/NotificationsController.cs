using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Interfaces;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? branchId,
        [FromQuery] bool unreadOnly = false)
    {
        var result = await _notificationService.GetNotificationsAsync(userId, branchId, unreadOnly);
        return Ok(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? branchId)
    {
        var result = await _notificationService.GetUnreadCountAsync(userId, branchId);
        return Ok(result);
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await _notificationService.MarkAsReadAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}
