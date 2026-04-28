using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Demand;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class DemandService : IDemandService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public DemandService(IPastryFlowDbContext context, IMapper mapper, INotificationService notificationService)
    {
        _context = context;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<DemandDto>> CreateDemandAsync(Guid userId, CreateDemandDto dto)
    {
        // Auto-generate DemandNumber: "DM-yyyyMMdd-NNN"
        string dateStr = DateTime.UtcNow.ToString("yyyyMMdd");
        var todayDemandsCount = await _context.Demands
            .Where(d => d.DemandNumber.StartsWith($"DM-{dateStr}"))
            .CountAsync();
            
        string demandNumber = $"DM-{dateStr}-{(todayDemandsCount + 1):D3}";

        var salesBranch = await _context.Branches.FindAsync(dto.SalesBranchId);

        var demand = new Demand
        {
            DemandNumber = demandNumber,
            SalesBranchId = dto.SalesBranchId,
            ProductionBranchId = dto.ProductionBranchId,
            Status = DemandStatus.Pending,
            Notes = dto.Notes,
            CreatedByUserId = userId,
            Items = dto.Items.Select(i => new DemandItem
            {
                ProductId = i.ProductId,
                RequestedQuantity = i.RequestedQuantity,
                Status = DemandItemStatus.Pending
            }).ToList()
        };

        _context.Demands.Add(demand);
        await _context.SaveChangesAsync();

        // Notification: Talep oluşturulduğunda üretim şubesine bildirim gönder
        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = demand.ProductionBranchId,
                Title = "Yeni Talep",
                Message = $"{salesBranch?.Name} şubesinden {demand.Items.Count} kalemlik yeni talep geldi.",
                Type = NotificationType.DemandCreated,
                SourceEntity = "Demand",
                SourceEntityId = demand.Id,
                SourceBranchId = demand.SalesBranchId,
                SourceBranchName = salesBranch?.Name
            });
        }
        catch (Exception) { /* Log and continue */ }

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<List<DemandDto>>> GetDemandsAsync(Guid? branchId = null, Guid? productionBranchId = null, DemandStatus? status = null, DateOnly? date = null)
    {
        var query = _context.Demands
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .Include(d => d.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();

        if (branchId.HasValue && !productionBranchId.HasValue)
        {
            query = query.Where(d => d.SalesBranchId == branchId.Value || d.ProductionBranchId == branchId.Value);
        }

        if (productionBranchId.HasValue)
        {
            query = query.Where(d => d.ProductionBranchId == productionBranchId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(d => d.Status == status.Value);
        }

        if (date.HasValue)
        {
            query = query.Where(d => DateOnly.FromDateTime(d.CreatedAt.Date) == date.Value);
        }

        var demands = await query.OrderByDescending(d => d.CreatedAt).ToListAsync();
        return ApiResponse<List<DemandDto>>.Ok(_mapper.Map<List<DemandDto>>(demands));
    }

    public async Task<ApiResponse<DemandDto>> GetDemandByIdAsync(Guid id)
    {
        var demand = await _context.Demands
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .Include(d => d.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        return ApiResponse<DemandDto>.Ok(_mapper.Map<DemandDto>(demand));
    }

    public async Task<ApiResponse<DemandDto>> ReviewDemandAsync(Guid id, ReviewDemandDto dto)
    {
        var demand = await _context.Demands
            .Include(d => d.Items)
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        if (demand.Status != DemandStatus.Pending)
            return ApiResponse<DemandDto>.Fail("Sadece bekleyen talepler incelenebilir.");

        var reviewedAt = DateTime.UtcNow;

        foreach (var reviewItem in dto.Items)
        {
            var demandItem = demand.Items.FirstOrDefault(i => i.Id == reviewItem.DemandItemId);
            if (demandItem == null) continue;

            if (reviewItem.Status == "Approved")
            {
                demandItem.Status = DemandItemStatus.Approved;
                demandItem.ApprovedQuantity = reviewItem.ApprovedQuantity ?? demandItem.RequestedQuantity;
                demandItem.RejectionReason = demandItem.ApprovedQuantity < demandItem.RequestedQuantity 
                    ? reviewItem.RejectionReason 
                    : null;
            }
            else if (reviewItem.Status == "Rejected")
            {
                demandItem.Status = DemandItemStatus.Rejected;
                demandItem.ApprovedQuantity = null;
                demandItem.RejectionReason = reviewItem.RejectionReason;
            }

            demandItem.ReviewedByUserId = dto.ReviewedByUserId;
            demandItem.ReviewedAt = reviewedAt;
        }

        bool allFullApproved = demand.Items.All(i => i.Status == DemandItemStatus.Approved && i.ApprovedQuantity == i.RequestedQuantity);
        bool allRejected = demand.Items.All(i => i.Status == DemandItemStatus.Rejected);

        demand.Status = allFullApproved ? DemandStatus.Approved :
                        allRejected ? DemandStatus.Rejected :
                        DemandStatus.PartiallyApproved;

        demand.ReviewedByUserId = dto.ReviewedByUserId;
        demand.ReviewedAt = reviewedAt;

        await _context.SaveChangesAsync();

        // Notification: Onay/Red sonrası
        try
        {
            var title = demand.Status == DemandStatus.Approved ? "Talep Onaylandı" :
                        demand.Status == DemandStatus.Rejected ? "Talep Reddedildi" :
                        "Talep Kısmen Onaylandı";

            var message = demand.Status == DemandStatus.Approved ? $"Talebiniz {demand.ProductionBranch?.Name} tarafından onaylandı." :
                          demand.Status == DemandStatus.Rejected ? $"Talebiniz {demand.ProductionBranch?.Name} tarafından reddedildi." :
                          $"Talebiniz {demand.ProductionBranch?.Name} tarafından kısmen onaylandı. {demand.Items.Count(i => i.Status == DemandItemStatus.Approved)}/{demand.Items.Count} kalem onaylandı.";

            var type = demand.Status == DemandStatus.Approved ? NotificationType.DemandApproved :
                       demand.Status == DemandStatus.Rejected ? NotificationType.DemandRejected :
                       NotificationType.DemandPartiallyApproved;

            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = demand.SalesBranchId,
                Title = title,
                Message = message,
                Type = type,
                SourceEntity = "Demand",
                SourceEntityId = demand.Id,
                SourceBranchId = demand.ProductionBranchId,
                SourceBranchName = demand.ProductionBranch?.Name
            });
        }
        catch (Exception) { }

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<DemandDto>> DeliverDemandAsync(Guid id, DeliverDemandDto dto)
    {
        var demand = await _context.Demands.Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
        if (demand == null) return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        var shipDto = new ShipDemandDto
        {
            Items = demand.Items
                .Where(i => i.Status == DemandItemStatus.Approved)
                .Select(i => new ShipDemandItemDto
                {
                    DemandItemId = i.Id,
                    SentQuantity = i.ApprovedQuantity ?? i.RequestedQuantity
                }).ToList()
        };

        return await ShipDemandAsync(id, shipDto, dto.DriverUserId ?? Guid.Empty);
    }

    public async Task<ApiResponse<DemandDto>> ShipDemandAsync(Guid demandId, ShipDemandDto dto, Guid currentUserId)
    {
        var demand = await _context.Demands
            .Include(d => d.Items)
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .FirstOrDefaultAsync(d => d.Id == demandId);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        if (demand.Status != DemandStatus.Approved && demand.Status != DemandStatus.PartiallyApproved)
            return ApiResponse<DemandDto>.Fail("Bu talep henüz onaylanmamış veya zaten gönderilmiş.");

        var sentAt = DateTime.UtcNow;
        int shippedItemCount = 0;

        foreach (var itemDto in dto.Items)
        {
            var item = demand.Items.FirstOrDefault(i => i.Id == itemDto.DemandItemId);
            if (item == null) continue;

            if (item.Status != DemandItemStatus.Approved) continue;

            item.SentQuantity = itemDto.SentQuantity;
            item.SentAt = sentAt;
            item.Status = DemandItemStatus.Shipped;
            shippedItemCount++;
        }

        if (shippedItemCount == 0)
            return ApiResponse<DemandDto>.Fail("En az bir ürün gönderilmelidir.");

        demand.Status = DemandStatus.Shipped;
        demand.DeliveredAt = sentAt;
        demand.DriverUserId = currentUserId;

        await _context.SaveChangesAsync();

        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = demand.SalesBranchId,
                Title = "Teslimat Gönderildi",
                Message = $"{demand.ProductionBranch?.Name} şubesinden {shippedItemCount} kalem ürün yola çıktı.",
                Type = NotificationType.DeliveryReady,
                SourceEntity = "Demand",
                SourceEntityId = demand.Id,
                SourceBranchId = demand.ProductionBranchId,
                SourceBranchName = demand.ProductionBranch?.Name
            });
        }
        catch (Exception) { }

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<DemandDto?>> GetLastDemandAsync(Guid salesBranchId, Guid productionBranchId)
    {
        var yesterday = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));

        var demand = await _context.Demands
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .Include(d => d.Items)
            .ThenInclude(i => i.Product)
            .Where(d => d.SalesBranchId == salesBranchId && d.ProductionBranchId == productionBranchId
                        && DateOnly.FromDateTime(d.CreatedAt.Date) == yesterday)
            .OrderByDescending(d => d.CreatedAt)
            .FirstOrDefaultAsync();

        if (demand == null)
            return ApiResponse<DemandDto?>.Ok(null);

        return ApiResponse<DemandDto?>.Ok(_mapper.Map<DemandDto>(demand));
    }

    public async Task<ApiResponse<DemandDto>> ReceiveDemandAsync(Guid id, ReceiveDemandDto dto)
    {
        var demand = await _context.Demands.Include(d => d.Items).FirstOrDefaultAsync(d => d.Id == id);
        if (demand == null) return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        var acceptDto = new AcceptDeliveryDto
        {
            Items = demand.Items
                .Where(i => i.Status == DemandItemStatus.Shipped || i.Status == DemandItemStatus.Delivered || i.Status == DemandItemStatus.Approved)
                .Select(i => new AcceptDeliveryItemDto
                {
                    DemandItemId = i.Id,
                    AcceptedQuantity = i.SentQuantity ?? i.ApprovedQuantity ?? i.RequestedQuantity
                }).ToList()
        };

        return await AcceptDeliveryAsync(id, acceptDto, dto.ReceivedByUserId);
    }

    public async Task<ApiResponse<DemandDto>> AcceptDeliveryAsync(Guid demandId, AcceptDeliveryDto dto, Guid userId)
    {
        var demand = await _context.Demands
            .Include(d => d.Items)
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .FirstOrDefaultAsync(d => d.Id == demandId);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        if (demand.Status != DemandStatus.Shipped && demand.Status != DemandStatus.Delivered && demand.Status != DemandStatus.Approved && demand.Status != DemandStatus.PartiallyApproved)
            return ApiResponse<DemandDto>.Fail("Sadece gönderilmiş veya onaylanmış talepler teslim alınabilir.");

        var acceptedAt = DateTime.UtcNow;
        DateOnly today = DateOnly.FromDateTime(acceptedAt);
        int rejectedCount = 0;

        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .FirstOrDefaultAsync(c => c.BranchId == demand.SalesBranchId && c.Date == today);

        if (closing == null)
        {
            closing = new DayClosing
            {
                BranchId = demand.SalesBranchId,
                Date = today,
                IsOpened = true,
                OpenedAt = DateTime.UtcNow
            };
            _context.DayClosings.Add(closing);
            await _context.SaveChangesAsync();
        }

        foreach (var itemDto in dto.Items)
        {
            var item = demand.Items.FirstOrDefault(i => i.Id == itemDto.DemandItemId);
            if (item == null) continue;

            var sentQty = item.SentQuantity ?? item.ApprovedQuantity ?? item.RequestedQuantity;
            
            item.AcceptedQuantity = itemDto.AcceptedQuantity;
            item.RejectedQuantity = Math.Max(0, sentQty - itemDto.AcceptedQuantity);
            item.DeliveryRejectionReason = itemDto.RejectionReason;
            item.AcceptedAt = acceptedAt;
            item.Status = DemandItemStatus.Received;

            if (item.RejectedQuantity > 0)
            {
                if (string.IsNullOrEmpty(itemDto.RejectionReason))
                    return ApiResponse<DemandDto>.Fail($"{item.ProductId} için red sebebi zorunludur.");

                var deliveryReturn = new DeliveryReturn
                {
                    DemandId = demand.Id,
                    DemandItemId = item.Id,
                    ProductId = item.ProductId,
                    FromBranchId = demand.SalesBranchId,
                    ToBranchId = demand.ProductionBranchId,
                    Quantity = item.RejectedQuantity.Value,
                    Reason = itemDto.RejectionReason,
                    Status = DeliveryReturnStatus.Created
                };
                _context.DeliveryReturns.Add(deliveryReturn);
                rejectedCount++;
            }

            var detail = await _context.DayClosingDetails
                .FirstOrDefaultAsync(d => d.DayClosingId == closing.Id && d.ProductId == item.ProductId);

            if (detail == null)
            {
                var previousDetail = await _context.DayClosingDetails
                    .Include(d => d.DayClosing)
                    .Where(d => d.DayClosing.BranchId == demand.SalesBranchId && d.ProductId == item.ProductId && d.DayClosing.Date < today && d.DayClosing.IsClosed)
                    .OrderByDescending(d => d.DayClosing.Date)
                    .FirstOrDefaultAsync();

                detail = new DayClosingDetail
                {
                    DayClosingId = closing.Id,
                    ProductId = item.ProductId,
                    OpeningStock = previousDetail?.CarryOverQuantity ?? 0,
                    ReceivedFromDemands = item.AcceptedQuantity.Value
                };
                _context.DayClosingDetails.Add(detail);
            }
            else
            {
                detail.ReceivedFromDemands += item.AcceptedQuantity.Value;
            }
        }

        demand.Status = DemandStatus.Received;
        demand.ReceivedAt = acceptedAt;
        demand.ReceivedByUserId = userId;

        await _context.SaveChangesAsync();

        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = demand.ProductionBranchId,
                Title = "Teslimat Teslim Alındı",
                Message = rejectedCount > 0 
                    ? $"{demand.SalesBranch?.Name} şubesi teslimatı aldı. {rejectedCount} kalem reddedildi."
                    : $"{demand.SalesBranch?.Name} şubesi teslimatı eksiksiz aldı.",
                Type = NotificationType.DeliveryReceived,
                SourceEntity = "Demand",
                SourceEntityId = demand.Id,
                SourceBranchId = demand.SalesBranchId,
                SourceBranchName = demand.SalesBranch?.Name
            });

            if (rejectedCount > 0)
            {
                await _notificationService.CreateAndSendAsync(new CreateNotificationDto
                {
                    TargetRole = "Admin",
                    Title = "Sevkiyat İade Bildirimi",
                    Message = $"{demand.SalesBranch?.Name} -> {demand.ProductionBranch?.Name} teslimatında {rejectedCount} kalem iade var.",
                    Type = NotificationType.DeliveryReceived,
                    SourceEntity = "Demand",
                    SourceEntityId = demand.Id,
                    SourceBranchId = demand.SalesBranchId,
                    SourceBranchName = demand.SalesBranch?.Name
                });
            }
        }
        catch (Exception) { }

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<string>> UpdateRejectionPhotoAsync(Guid itemId, string photoUrl)
    {
        var item = await _context.DemandItems.FindAsync(itemId);
        if (item == null) return ApiResponse<string>.Fail("Ürün bulunamadı.");

        item.RejectionPhotoUrl = photoUrl;

        var deliveryReturn = await _context.DeliveryReturns.FirstOrDefaultAsync(r => r.DemandItemId == itemId);
        if (deliveryReturn != null)
        {
            deliveryReturn.PhotoUrl = photoUrl;
        }

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok(photoUrl, "Fotoğraf güncellendi.");
    }
}
