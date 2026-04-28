using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class CustomCakeOrderService : ICustomCakeOrderService
{
    private readonly IPastryFlowDbContext _context;
    private readonly INotificationService _notificationService;

    public CustomCakeOrderService(IPastryFlowDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    private async Task<string> GenerateOrderNumberAsync()
    {
        var year = DateTime.UtcNow.Year;
        var prefix = $"CCO-{year}-";
        
        var lastOrder = await _context.CustomCakeOrders
            .IgnoreQueryFilters()
            .Where(o => o.OrderNumber.StartsWith(prefix))
            .OrderByDescending(o => o.OrderNumber)
            .FirstOrDefaultAsync();
        
        int nextNumber = 1;
        if (lastOrder != null)
        {
            var lastNumberStr = lastOrder.OrderNumber.Replace(prefix, "");
            if (int.TryParse(lastNumberStr, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }
        
        return $"{prefix}{nextNumber:D4}";
    }

    private CustomCakeOrderDto MapToDto(CustomCakeOrder order)
    {
        return new CustomCakeOrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            BranchId = order.BranchId,
            BranchName = order.Branch?.Name ?? string.Empty,
            ProductionBranchId = order.ProductionBranchId,
            ProductionBranchName = order.ProductionBranch?.Name ?? string.Empty,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            DeliveryDate = order.DeliveryDate.ToString("yyyy-MM-dd"),
            ServingSize = order.ServingSize,
            CakeTypeId = order.CakeTypeId,
            CakeTypeName = order.CakeType?.Name ?? string.Empty,
            InnerCreamId = order.InnerCreamId,
            InnerCreamName = order.InnerCream?.Name ?? string.Empty,
            OuterCreamId = order.OuterCreamId,
            OuterCreamName = order.OuterCream?.Name ?? string.Empty,
            Description = order.Description,
            ReferencePhotoUrl = order.ReferencePhotoUrl,
            Price = order.Price,
            Status = order.Status.ToString(),
            StatusText = order.Status switch
            {
                CustomCakeOrderStatus.Created => "Oluşturuldu",
                CustomCakeOrderStatus.SentToProduction => "Üretime Gönderildi",
                CustomCakeOrderStatus.InProduction => "Üretimde",
                CustomCakeOrderStatus.Ready => "Hazır",
                CustomCakeOrderStatus.Delivered => "Teslim Edildi",
                CustomCakeOrderStatus.Cancelled => "İptal Edildi",
                _ => order.Status.ToString()
            },
            StatusNote = order.StatusNote,
            StatusChangedAt = order.StatusChangedAt,
            CreatedByUserName = order.CreatedByUser?.FullName ?? string.Empty,
            CreatedAt = order.CreatedAt
        };
    }

    private IQueryable<CustomCakeOrder> GetBaseQuery()
    {
        return _context.CustomCakeOrders
            .Include(o => o.Branch)
            .Include(o => o.ProductionBranch)
            .Include(o => o.CreatedByUser)
            .Include(o => o.CakeType)
            .Include(o => o.InnerCream)
            .Include(o => o.OuterCream);
    }

    public async Task<ApiResponse<CustomCakeOrderDto>> CreateAsync(CreateCustomCakeOrderDto dto, Guid userId, Guid branchId)
    {
        if (dto.DeliveryDate.Date < DateTime.UtcNow.Date)
            return ApiResponse<CustomCakeOrderDto>.Fail("Teslimat tarihi geçmişte olamaz.");

        var productionBranch = await _context.Branches
            .FirstOrDefaultAsync(b => b.BranchType == BranchType.Production && (b.City == City.Luleburgaz || b.Name.Contains("Lüleburgaz")));

        if (productionBranch == null)
            return ApiResponse<CustomCakeOrderDto>.Fail("Lüleburgaz Mutfak bulunamadı.");

        var order = new CustomCakeOrder
        {
            OrderNumber = await GenerateOrderNumberAsync(),
            BranchId = branchId,
            CreatedByUserId = userId,
            ProductionBranchId = productionBranch.Id,
            CustomerName = dto.CustomerName,
            CustomerPhone = dto.CustomerPhone,
            DeliveryDate = DateTime.SpecifyKind(dto.DeliveryDate, DateTimeKind.Utc),
            ServingSize = dto.ServingSize,
            CakeTypeId = dto.CakeTypeId,
            InnerCreamId = dto.InnerCreamId,
            OuterCreamId = dto.OuterCreamId,
            Description = dto.Description,
            Price = dto.Price,
            Status = CustomCakeOrderStatus.SentToProduction, // Directly to production as per rules
            StatusChangedAt = DateTime.UtcNow,
            StatusChangedByUserId = userId,
            StatusNote = "Sipariş oluşturuldu ve üretime gönderildi."
        };

        _context.CustomCakeOrders.Add(order);
        await _context.SaveChangesAsync();

        var createdOrder = await GetBaseQuery().FirstOrDefaultAsync(o => o.Id == order.Id);

        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = productionBranch.Id,
                Title = "Yeni Özel Pasta Siparişi",
                Message = $"{createdOrder?.Branch?.Name} şubesinden {order.OrderNumber} numaralı yeni özel pasta siparişi geldi.",
                Type = NotificationType.CakeOrderCreated,
                SourceEntity = "CustomCakeOrder",
                SourceEntityId = order.Id,
                SourceBranchId = branchId,
                SourceBranchName = createdOrder?.Branch?.Name
            });
        }
        catch (Exception) { }

        return ApiResponse<CustomCakeOrderDto>.Ok(MapToDto(createdOrder!));
    }

    public async Task<ApiResponse<List<CustomCakeOrderDto>>> GetByBranchAsync(Guid branchId, CustomCakeOrderStatus? status = null)
    {
        var query = GetBaseQuery().Where(o => o.BranchId == branchId);
        
        if (status.HasValue)
            query = query.Where(o => o.Status == status.Value);

        var orders = await query.OrderByDescending(o => o.CreatedAt).ToListAsync();
        return ApiResponse<List<CustomCakeOrderDto>>.Ok(orders.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<List<CustomCakeOrderDto>>> GetByProductionBranchAsync(Guid productionBranchId, CustomCakeOrderStatus? status = null)
    {
        var query = GetBaseQuery().Where(o => o.ProductionBranchId == productionBranchId);
        
        if (status.HasValue)
            query = query.Where(o => o.Status == status.Value);

        var orders = await query.OrderByDescending(o => o.DeliveryDate).ToListAsync();
        return ApiResponse<List<CustomCakeOrderDto>>.Ok(orders.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<List<CustomCakeOrderDto>>> GetAllAsync(CustomCakeOrderStatus? status = null)
    {
        var query = GetBaseQuery();
        
        if (status.HasValue)
            query = query.Where(o => o.Status == status.Value);

        var orders = await query.OrderByDescending(o => o.CreatedAt).ToListAsync();
        return ApiResponse<List<CustomCakeOrderDto>>.Ok(orders.Select(MapToDto).ToList());
    }

    public async Task<ApiResponse<CustomCakeOrderDto>> GetByIdAsync(Guid id)
    {
        var order = await GetBaseQuery().FirstOrDefaultAsync(o => o.Id == id);
        
        if (order == null)
            return ApiResponse<CustomCakeOrderDto>.Fail("Sipariş bulunamadı.");

        return ApiResponse<CustomCakeOrderDto>.Ok(MapToDto(order));
    }

    public async Task<ApiResponse<CustomCakeOrderDto>> UpdateStatusAsync(Guid id, UpdateCakeOrderStatusDto dto, Guid userId)
    {
        var order = await GetBaseQuery().FirstOrDefaultAsync(o => o.Id == id);
        if (order == null) return ApiResponse<CustomCakeOrderDto>.Fail("Sipariş bulunamadı.");

        // Transitions validation
        bool validTransition = false;
        switch (order.Status)
        {
            case CustomCakeOrderStatus.Created:
                validTransition = dto.NewStatus == CustomCakeOrderStatus.SentToProduction || dto.NewStatus == CustomCakeOrderStatus.Cancelled;
                break;
            case CustomCakeOrderStatus.SentToProduction:
                validTransition = dto.NewStatus == CustomCakeOrderStatus.InProduction;
                break;
            case CustomCakeOrderStatus.InProduction:
                validTransition = dto.NewStatus == CustomCakeOrderStatus.Ready;
                break;
            case CustomCakeOrderStatus.Ready:
                validTransition = dto.NewStatus == CustomCakeOrderStatus.Delivered;
                break;
        }

        if (!validTransition && dto.NewStatus != order.Status) // Admin can override potentially, but sticking to rules
        {
            var user = await _context.Users.FindAsync(userId);
            if (user?.Role != UserRole.Admin)
            {
                return ApiResponse<CustomCakeOrderDto>.Fail($"{order.Status} durumundan {dto.NewStatus} durumuna geçiş yapılamaz.");
            }
        }

        if (order.Status != dto.NewStatus)
        {
            order.Status = dto.NewStatus;
            order.StatusNote = dto.StatusNote;
            order.StatusChangedAt = DateTime.UtcNow;
            order.StatusChangedByUserId = userId;
            
            await _context.SaveChangesAsync();

            // Notifications
            try
            {
                if (dto.NewStatus == CustomCakeOrderStatus.InProduction)
                {
                    await _notificationService.CreateAndSendAsync(new CreateNotificationDto
                    {
                        BranchId = order.BranchId,
                        Title = "Sipariş Üretime Başladı",
                        Message = $"{order.OrderNumber} numaralı pasta siparişiniz üretim aşamasındadır.",
                        Type = NotificationType.CakeOrderStatusChanged,
                        SourceEntity = "CustomCakeOrder",
                        SourceEntityId = order.Id,
                        SourceBranchId = order.ProductionBranchId,
                        SourceBranchName = order.ProductionBranch?.Name
                    });
                }
                else if (dto.NewStatus == CustomCakeOrderStatus.Ready)
                {
                    await _notificationService.CreateAndSendAsync(new CreateNotificationDto
                    {
                        BranchId = order.BranchId,
                        Title = "Sipariş Hazır",
                        Message = $"{order.OrderNumber} numaralı pasta siparişiniz teslimata hazırdır.",
                        Type = NotificationType.CakeOrderStatusChanged,
                        SourceEntity = "CustomCakeOrder",
                        SourceEntityId = order.Id,
                        SourceBranchId = order.ProductionBranchId,
                        SourceBranchName = order.ProductionBranch?.Name
                    });
                }
                else if (dto.NewStatus == CustomCakeOrderStatus.Delivered)
                {
                    await _notificationService.CreateAndSendAsync(new CreateNotificationDto
                    {
                        TargetRole = "Admin",
                        Title = "Sipariş Teslim Edildi",
                        Message = $"{order.OrderNumber} numaralı pasta siparişi {order.Branch?.Name} şubesine teslim edildi.",
                        Type = NotificationType.CakeOrderStatusChanged,
                        SourceEntity = "CustomCakeOrder",
                        SourceEntityId = order.Id,
                        SourceBranchId = order.BranchId,
                        SourceBranchName = order.Branch?.Name
                    });
                }
            }
            catch (Exception) { }
        }

        return ApiResponse<CustomCakeOrderDto>.Ok(MapToDto(order));
    }

    public async Task<ApiResponse<string>> UploadReferencePhotoAsync(Guid id, IFormFile photo, string webRootPath)
    {
        var order = await _context.CustomCakeOrders.FindAsync(id);
        if (order == null) return ApiResponse<string>.Fail("Sipariş bulunamadı.");

        var photoUrl = await FileUploadHelper.SaveFileAsync(photo, "cake-orders", webRootPath);
        
        order.ReferencePhotoUrl = photoUrl;
        await _context.SaveChangesAsync();

        return ApiResponse<string>.Ok(photoUrl, "Referans fotoğraf yüklendi.");
    }

    public async Task<ApiResponse<bool>> CancelAsync(Guid id, Guid userId)
    {
        var order = await _context.CustomCakeOrders.FindAsync(id);
        if (order == null) return ApiResponse<bool>.Fail("Sipariş bulunamadı.");

        if (order.Status != CustomCakeOrderStatus.Created && order.Status != CustomCakeOrderStatus.SentToProduction)
            return ApiResponse<bool>.Fail("Sadece yeni oluşturulan siparişler iptal edilebilir.");

        order.Status = CustomCakeOrderStatus.Cancelled;
        order.StatusNote = "Tezgah tarafından iptal edildi.";
        order.StatusChangedAt = DateTime.UtcNow;
        order.StatusChangedByUserId = userId;

        await _context.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true, "Sipariş iptal edildi.");
    }
}
