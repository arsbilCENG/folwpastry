using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Waste;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class WasteService : IWasteService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStockService _stockService;
    private readonly INotificationService _notificationService;
    private readonly IDayClosingService _dayClosingService;

    public WasteService(
        IPastryFlowDbContext context, 
        IMapper mapper, 
        IStockService stockService,
        INotificationService notificationService,
        IDayClosingService dayClosingService)
    {
        _context = context;
        _mapper = mapper;
        _stockService = stockService;
        _notificationService = notificationService;
        _dayClosingService = dayClosingService;
    }

    public async Task<ApiResponse<WasteDto>> CreateWasteAsync(CreateWasteDto dto, Guid createdByUserId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);
        if (product == null)
            return ApiResponse<WasteDto>.Fail("Ürün bulunamadı.");

        // KURAL 1: Counter Ürünler Stock Kaydı ALMAZ
        if (product.TrackingType != TrackingType.Counter)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BranchId == dto.BranchId && s.ProductId == dto.ProductId);

            if (stock == null)
                return ApiResponse<WasteDto>.Fail("Ürüne ait stok kaydı bulunamadı.");

            if (dto.Quantity > stock.CurrentQuantity)
                return ApiResponse<WasteDto>.Fail($"Zayiat miktarı mevcut stoktan ( {stock.CurrentQuantity} ) fazla olamaz.");

            stock.CurrentQuantity -= dto.Quantity;
            stock.UpdatedAt = DateTime.UtcNow;
        }

        var waste = new Waste
        {
            BranchId = dto.BranchId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            WasteType = WasteType.DuringDay,
            Notes = dto.Notes,
            Date = dto.Date,
            PhotoPath = dto.PhotoPath,
            CreatedByUserId = createdByUserId
        };

        _context.Wastes.Add(waste);

        // Update DailyStockSummary
        var closing = await _dayClosingService.GetOrCreateDayClosingAsync(dto.BranchId, dto.Date);

        var detail = closing.Details.FirstOrDefault(d => d.ProductId == dto.ProductId);
        if (detail == null)
        {
            var previousDetail = await _context.DayClosingDetails
                .Include(d => d.DayClosing)
                .Where(d => d.DayClosing.BranchId == dto.BranchId && d.ProductId == dto.ProductId && d.DayClosing.Date < dto.Date && d.DayClosing.IsClosed)
                .OrderByDescending(d => d.DayClosing.Date)
                .FirstOrDefaultAsync();

            detail = new DayClosingDetail
            {
                DayClosingId = closing.Id,
                ProductId = dto.ProductId,
                OpeningStock = previousDetail?.CarryOverQuantity ?? 0,
                DayWasteQuantity = dto.Quantity
            };
            _context.DayClosingDetails.Add(detail);
        }
        else
        {
            detail.DayWasteQuantity += dto.Quantity;
        }

        await _context.SaveChangesAsync();

        var createdWaste = await _context.Wastes
            .Include(w => w.Branch)
            .Include(w => w.Product)
            .FirstOrDefaultAsync(w => w.Id == waste.Id);

        // Admin'e bildirim
        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                TargetRole = "Admin",
                Title = "Zayiat Kaydedildi",
                Message = $"{createdWaste?.Branch?.Name} şubesinde {waste.Quantity} {createdWaste?.Product?.Unit} {createdWaste?.Product?.Name} zayiat kaydedildi.",
                Type = NotificationType.WasteRecorded,
                SourceEntity = "Waste",
                SourceEntityId = waste.Id,
                SourceBranchId = createdWaste?.BranchId,
                SourceBranchName = createdWaste?.Branch?.Name
            });
        }
        catch (Exception) { }

        return ApiResponse<WasteDto>.Ok(_mapper.Map<WasteDto>(createdWaste));
    }

    public async Task<ApiResponse<List<WasteDto>>> GetWastesAsync(Guid branchId, DateOnly date)
    {
        var wastes = await _context.Wastes
            .Include(w => w.Branch)
            .Include(w => w.Product)
            .Where(w => w.BranchId == branchId && w.Date == date && w.WasteType == WasteType.DuringDay)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();

        return ApiResponse<List<WasteDto>>.Ok(_mapper.Map<List<WasteDto>>(wastes));
    }
}
