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

    public WasteService(
        IPastryFlowDbContext context, 
        IMapper mapper, 
        IStockService stockService,
        INotificationService notificationService)
    {
        _context = context;
        _mapper = mapper;
        _stockService = stockService;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<WasteDto>> CreateWasteAsync(CreateWasteDto dto, Guid createdByUserId)
    {
        // Validation: quantity must not exceed current stock
        var stockResponse = await _stockService.GetCurrentStockAsync(dto.BranchId, dto.Date);
        if (!stockResponse.Success || stockResponse.Data == null)
            return ApiResponse<WasteDto>.Fail("Stok bilgisi alınamadı.");

        var productStock = stockResponse.Data.FirstOrDefault(s => s.ProductId == dto.ProductId);
        if (productStock == null)
            return ApiResponse<WasteDto>.Fail("Ürüne ait günlük stok kaydı bulunamadı.");

        if (dto.Quantity > productStock.CurrentStock)
            return ApiResponse<WasteDto>.Fail($"Zayiat miktarı mevcut stoktan ( {productStock.CurrentStock} ) fazla olamaz.");

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
        var detail = await _context.DayClosingDetails
            .Include(d => d.DayClosing)
            .FirstOrDefaultAsync(d => d.DayClosing.BranchId == dto.BranchId && d.ProductId == dto.ProductId && d.DayClosing.Date == dto.Date);

        if (detail != null)
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
