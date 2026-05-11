using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.DTOs.Transfer;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class TransferService : ITransferService
{
    private readonly IPastryFlowDbContext _context;
    private readonly INotificationService _notificationService;

    public TransferService(IPastryFlowDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<TransferDto> CreateTransferAsync(CreateTransferRequest request, Guid senderBranchId, Guid userId)
    {
        if (senderBranchId == request.ReceiverBranchId)
            throw new Exception("Şube kendi kendine transfer yapamaz.");

        if (request.Items == null || !request.Items.Any())
            throw new Exception("En az bir ürün eklemelisiniz.");

        var senderBranch = await _context.Branches.FindAsync(senderBranchId) ?? throw new Exception("Gönderen şube bulunamadı.");
        var receiverBranch = await _context.Branches.FindAsync(request.ReceiverBranchId) ?? throw new Exception("Alıcı şube bulunamadı.");

        var year = DateTime.UtcNow.Year;
        var lastNumber = await _context.Transfers
            .Where(t => t.TransferNumber.StartsWith($"TRF-{year}-"))
            .CountAsync();
        var transferNumber = $"TRF-{year}-{(lastNumber + 1):D4}";

        var transfer = new Transfer
        {
            Id = Guid.NewGuid(),
            TransferNumber = transferNumber,
            SenderBranchId = senderBranchId,
            ReceiverBranchId = request.ReceiverBranchId,
            Status = TransferStatus.Shipped,
            ShippedAt = DateTime.UtcNow,
            Notes = request.Notes,
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var itemDto in request.Items)
        {
            if (itemDto.Quantity <= 0)
                throw new Exception("Miktar 0'dan büyük olmalıdır.");

            var product = await _context.Products.FindAsync(itemDto.ProductId) ?? throw new Exception("Ürün bulunamadı.");

            if (product.TrackingType == TrackingType.Counter)
                throw new Exception($"'{product.Name}' tezgah ürünü olduğu için transfer edilemez.");

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BranchId == senderBranchId && s.ProductId == itemDto.ProductId);

            if (stock == null || stock.CurrentQuantity < itemDto.Quantity)
            {
                var qty = stock?.CurrentQuantity ?? 0;
                throw new Exception($"'{product.Name}' için yetersiz stok. Mevcut: {qty}");
            }

            stock.CurrentQuantity -= itemDto.Quantity;
            stock.UpdatedAt = DateTime.UtcNow;

            transfer.Items.Add(new TransferItem
            {
                Id = Guid.NewGuid(),
                TransferId = transfer.Id,
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        _context.Transfers.Add(transfer);
        await _context.SaveChangesAsync();

        try
        {
            // Alıcı şubeye bildirim
            await _notificationService.CreateAndSendAsync(new DTOs.Notifications.CreateNotificationDto
            {
                BranchId = receiverBranch.Id,
                Type = NotificationType.TransferShipped,
                Title = "Yeni Transfer",
                Message = $"{senderBranch.Name} şubesinden {transfer.Items.Count} kalem ürün transferi gönderildi.",
                SourceEntity = "Transfer",
                SourceEntityId = transfer.Id,
                SourceBranchId = senderBranch.Id,
                SourceBranchName = senderBranch.Name
            });
        }
        catch
        {
            // Ignore notification errors
        }

        return await GetTransferByIdAsync(transfer.Id);
    }

    public async Task<TransferDto> ReceiveTransferAsync(Guid transferId, Guid receiverBranchId, Guid userId)
    {
        var transfer = await _context.Transfers
            .Include(t => t.Items)
            .Include(t => t.SenderBranch)
            .Include(t => t.ReceiverBranch)
            .FirstOrDefaultAsync(t => t.Id == transferId) ?? throw new Exception("Transfer bulunamadı.");

        if (transfer.Status != TransferStatus.Shipped)
            throw new Exception("Sadece yolda olan transferler teslim alınabilir.");

        if (transfer.ReceiverBranchId != receiverBranchId)
            throw new Exception("Bu transferi sadece alıcı şube teslim alabilir.");

        foreach (var item in transfer.Items)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BranchId == receiverBranchId && s.ProductId == item.ProductId);

            if (stock == null)
            {
                stock = new Stock
                {
                    Id = Guid.NewGuid(),
                    BranchId = receiverBranchId,
                    ProductId = item.ProductId,
                    CurrentQuantity = item.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Stocks.Add(stock);
            }
            else
            {
                stock.CurrentQuantity += item.Quantity;
                stock.UpdatedAt = DateTime.UtcNow;
            }
        }

        transfer.Status = TransferStatus.Received;
        transfer.ReceivedAt = DateTime.UtcNow;
        transfer.ReceivedByUserId = userId;
        transfer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        try
        {
            // Gönderen şubeye bildirim
            await _notificationService.CreateAndSendAsync(new DTOs.Notifications.CreateNotificationDto
            {
                BranchId = transfer.SenderBranchId,
                Type = NotificationType.TransferReceived,
                Title = "Transfer Teslim Alındı",
                Message = $"{transfer.ReceiverBranch.Name} şubesi transferi teslim aldı. Transfer No: {transfer.TransferNumber}",
                SourceEntity = "Transfer",
                SourceEntityId = transfer.Id,
                SourceBranchId = transfer.ReceiverBranchId,
                SourceBranchName = transfer.ReceiverBranch.Name
            });
        }
        catch
        {
            // Ignore notification errors
        }

        return await GetTransferByIdAsync(transfer.Id);
    }

    public async Task CancelTransferAsync(Guid transferId, Guid requestingBranchId, Guid userId, CancelTransferRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        bool isAdmin = user?.Role == UserRole.Admin;

        var transfer = await _context.Transfers
            .Include(t => t.Items)
            .FirstOrDefaultAsync(t => t.Id == transferId) ?? throw new Exception("Transfer bulunamadı.");

        if (transfer.Status != TransferStatus.Shipped)
            throw new Exception("Sadece yolda olan transferler iptal edilebilir.");

        if (transfer.SenderBranchId != requestingBranchId && !isAdmin)
            throw new Exception("Sadece gönderen şube veya admin transferi iptal edebilir.");

        if (string.IsNullOrWhiteSpace(request.CancellationReason))
            throw new Exception("İptal sebebi boş olamaz.");

        foreach (var item in transfer.Items)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BranchId == transfer.SenderBranchId && s.ProductId == item.ProductId);

            if (stock != null)
            {
                stock.CurrentQuantity += item.Quantity;
                stock.UpdatedAt = DateTime.UtcNow;
            }
        }

        transfer.Status = TransferStatus.Cancelled;
        transfer.CancelledAt = DateTime.UtcNow;
        transfer.CancelledByUserId = userId;
        transfer.CancellationReason = request.CancellationReason;
        transfer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<List<TransferDto>> GetOutgoingTransfersAsync(Guid branchId, TransferStatus? status = null)
    {
        var query = _context.Transfers
            .Include(t => t.SenderBranch)
            .Include(t => t.ReceiverBranch)
            .Include(t => t.CreatedBy)
            .Include(t => t.ReceivedBy)
            .Include(t => t.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Category)
            .Where(t => t.SenderBranchId == branchId && !t.IsDeleted);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        var list = await query.OrderByDescending(t => t.ShippedAt).ToListAsync();
        return list.Select(MapToDto).ToList();
    }

    public async Task<List<TransferDto>> GetIncomingTransfersAsync(Guid branchId, TransferStatus? status = null)
    {
        var query = _context.Transfers
            .Include(t => t.SenderBranch)
            .Include(t => t.ReceiverBranch)
            .Include(t => t.CreatedBy)
            .Include(t => t.ReceivedBy)
            .Include(t => t.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Category)
            .Where(t => t.ReceiverBranchId == branchId && !t.IsDeleted);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        var list = await query.OrderByDescending(t => t.ShippedAt).ToListAsync();
        return list.Select(MapToDto).ToList();
    }

    public async Task<List<TransferDto>> GetAllTransfersAsync(TransferStatus? status = null)
    {
        var query = _context.Transfers
            .Include(t => t.SenderBranch)
            .Include(t => t.ReceiverBranch)
            .Include(t => t.CreatedBy)
            .Include(t => t.ReceivedBy)
            .Include(t => t.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Category)
            .Where(t => !t.IsDeleted);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        var list = await query.OrderByDescending(t => t.ShippedAt).ToListAsync();
        return list.Select(MapToDto).ToList();
    }

    public async Task<TransferDto> GetTransferByIdAsync(Guid transferId)
    {
        var transfer = await _context.Transfers
            .Include(t => t.SenderBranch)
            .Include(t => t.ReceiverBranch)
            .Include(t => t.CreatedBy)
            .Include(t => t.ReceivedBy)
            .Include(t => t.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(t => t.Id == transferId && !t.IsDeleted) ?? throw new Exception("Transfer bulunamadı.");

        return MapToDto(transfer);
    }

    private static string GetStatusLabel(TransferStatus status) => status switch
    {
        TransferStatus.Shipped => "Yolda",
        TransferStatus.Received => "Teslim Alındı",
        TransferStatus.Cancelled => "İptal Edildi",
        _ => "Bilinmiyor"
    };

    private static TransferDto MapToDto(Transfer t) => new()
    {
        Id = t.Id,
        TransferNumber = t.TransferNumber,
        SenderBranchId = t.SenderBranchId,
        SenderBranchName = t.SenderBranch?.Name ?? string.Empty,
        ReceiverBranchId = t.ReceiverBranchId,
        ReceiverBranchName = t.ReceiverBranch?.Name ?? string.Empty,
        Status = t.Status,
        StatusLabel = GetStatusLabel(t.Status),
        ShippedAt = t.ShippedAt,
        ReceivedAt = t.ReceivedAt,
        CancelledAt = t.CancelledAt,
        Notes = t.Notes,
        CancellationReason = t.CancellationReason,
        CreatedByName = t.CreatedBy?.FullName ?? string.Empty,
        ReceivedByName = t.ReceivedBy?.FullName,
        Items = t.Items.Where(i => !i.IsDeleted).Select(i => new TransferItemDto
        {
            ProductId = i.ProductId,
            ProductName = i.Product?.Name ?? string.Empty,
            CategoryName = i.Product?.Category?.Name ?? string.Empty,
            Unit = i.Product?.Unit.ToString() ?? string.Empty,
            Quantity = i.Quantity
        }).ToList()
    };
}
