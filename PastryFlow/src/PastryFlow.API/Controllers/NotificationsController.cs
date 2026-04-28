using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Interfaces;
using PastryFlow.Application.Common;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUserNotifications(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 20,
        [FromQuery] bool? isRead = null)
    {
        var userId = GetCurrentUserId();
        var branchId = GetCurrentBranchId();
        var role = GetCurrentRole();

        var result = await _notificationService.GetUserNotificationsAsync(userId, branchId, role, pageNumber, pageSize, isRead);
        return Ok(ApiResponse<object>.Ok(result));
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = GetCurrentUserId();
        var branchId = GetCurrentBranchId();
        var role = GetCurrentRole();

        var count = await _notificationService.GetUnreadCountAsync(userId, branchId, role);
        return Ok(ApiResponse<int>.Ok(count));
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var userId = GetCurrentUserId();
        await _notificationService.MarkAsReadAsync(id, userId);
        return Ok(ApiResponse<bool>.Ok(true));
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetCurrentUserId();
        var branchId = GetCurrentBranchId();
        var role = GetCurrentRole();

        await _notificationService.MarkAllAsReadAsync(userId, branchId, role);
        return Ok(ApiResponse<bool>.Ok(true));
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                        ?? User.FindFirst("sub")?.Value;
        return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
    }

    private Guid? GetCurrentBranchId()
    {
        var branchIdClaim = User.FindFirst("BranchId")?.Value;
        return branchIdClaim != null ? Guid.Parse(branchIdClaim) : null;
    }

    private string GetCurrentRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? "";
    }
}
