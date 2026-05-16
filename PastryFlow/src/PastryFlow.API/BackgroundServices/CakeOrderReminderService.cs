using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.BackgroundServices;

public class CakeOrderReminderService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CakeOrderReminderService> _logger;

    public CakeOrderReminderService(IServiceScopeFactory scopeFactory, ILogger<CakeOrderReminderService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Cake Order Reminder Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            // Türkiye saati (UTC+3) 09:00, UTC 06:00'a denk gelir.
            // Eğer saat 06:00 ise çalıştır, değilse bir sonraki kontrolü bekle.
            
            // Basitlik için her saat başı kontrol edip, 06:00:00 - 06:59:59 arasındaysa işlemi yapalım
            // ve o gün bir daha yapmamak için flag tutalım veya bir sonraki günü bekleyelim.
            
            if (now.Hour == 6)
            {
                try
                {
                    await SendRemindersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending cake order reminders.");
                }
                
                // Bir sonraki güne kadar bekle (23 saat civarı)
                await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
            }
            else
            {
                // Her 15 dakikada bir kontrol et
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }

    private async Task SendRemindersAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IPastryFlowDbContext>();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        var dayAfterTomorrow = today.AddDays(2);

        var ordersToRemind = await context.CustomCakeOrders
            .Include(o => o.Branch)
            .Include(o => o.ProductionBranch)
            .Where(o => o.Status != CustomCakeOrderStatus.Delivered && o.Status != CustomCakeOrderStatus.Cancelled)
            .Where(o => o.DeliveryDate.Date == tomorrow || o.DeliveryDate.Date == dayAfterTomorrow)
            .ToListAsync();

        _logger.LogInformation("Found {Count} orders for delivery reminder.", ordersToRemind.Count);

        foreach (var order in ordersToRemind)
        {
            var daysLeft = (order.DeliveryDate.Date - today).Days;
            var timeLabel = daysLeft == 1 ? "yarın" : "2 gün sonra";
            
            var message = $"⚠️ {order.OrderNumber} numaralı pasta siparişi {order.DeliveryDate:dd.MM.yyyy} tarihinde ({timeLabel}) teslim edilecek. ({order.CustomerName})";

            // Siparişi alan şubeye bildirim
            try
            {
                await notificationService.CreateAndSendAsync(new CreateNotificationDto
                {
                    BranchId = order.BranchId,
                    Title = "Teslimat Hatırlatması",
                    Message = message,
                    Type = NotificationType.CakeOrderDeliveryReminder,
                    SourceEntity = "CustomCakeOrder",
                    SourceEntityId = order.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not send reminder to branch {BranchId} for order {OrderNumber}", order.BranchId, order.OrderNumber);
            }

            // Üretim şubesine bildirim
            try
            {
                await notificationService.CreateAndSendAsync(new CreateNotificationDto
                {
                    BranchId = order.ProductionBranchId,
                    Title = "Teslimat Hatırlatması (Üretim)",
                    Message = message,
                    Type = NotificationType.CakeOrderDeliveryReminder,
                    SourceEntity = "CustomCakeOrder",
                    SourceEntityId = order.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not send reminder to production branch {BranchId} for order {OrderNumber}", order.ProductionBranchId, order.OrderNumber);
            }
        }
    }
}
