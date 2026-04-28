using System;
using System.Threading.Tasks;
using PastryFlow.Application.DTOs.Notifications;

namespace PastryFlow.Application.Interfaces;

public interface INotificationService
{
    // Bildirim oluştur + SignalR ile gönder
    Task CreateAndSendAsync(CreateNotificationDto dto);
    
    // Kullanıcının bildirimlerini getir
    Task<NotificationListDto> GetUserNotificationsAsync(
        Guid userId, 
        Guid? branchId, 
        string role,
        int pageNumber = 1, 
        int pageSize = 20,
        bool? isRead = null);
    
    // Okunmamış bildirim sayısı
    Task<int> GetUnreadCountAsync(Guid userId, Guid? branchId, string role);
    
    // Tek bildirimi okundu işaretle
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
    
    // Tüm bildirimleri okundu işaretle
    Task MarkAllAsReadAsync(Guid userId, Guid? branchId, string role);
}
