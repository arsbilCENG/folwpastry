using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Waste;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class WasteService : IWasteService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStockService _stockService;

    public WasteService(IPastryFlowDbContext context, IMapper mapper, IStockService stockService)
    {
        _context = context;
        _mapper = mapper;
        _stockService = stockService;
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
