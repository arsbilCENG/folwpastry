using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Demand;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class DemandService : IDemandService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;

    public DemandService(IPastryFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<DemandDto>> CreateDemandAsync(Guid userId, CreateDemandDto dto)
    {
        // Auto-generate DemandNumber: "DM-yyyyMMdd-NNN"
        string dateStr = DateTime.UtcNow.ToString("yyyyMMdd");
        var todayDemandsCount = await _context.Demands
            .Where(d => d.DemandNumber.StartsWith($"DM-{dateStr}"))
            .CountAsync();
            
        string demandNumber = $"DM-{dateStr}-{(todayDemandsCount + 1):D3}";

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

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<List<DemandDto>>> GetDemandsAsync(Guid? branchId = null, DemandStatus? status = null, DateOnly? date = null)
    {
        var query = _context.Demands
            .Include(d => d.SalesBranch)
            .Include(d => d.ProductionBranch)
            .Include(d => d.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();

        if (branchId.HasValue)
        {
            query = query.Where(d => d.SalesBranchId == branchId.Value || d.ProductionBranchId == branchId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(d => d.Status == status.Value);
        }

        if (date.HasValue)
        {
            // Assuming CreatedAt is UTC, comparing date part
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

    public async Task<ApiResponse<DemandDto>> ReceiveDemandAsync(Guid id, ReceiveDemandDto dto)
    {
        var demand = await _context.Demands
            .Include(d => d.Items)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        if (demand.Status != DemandStatus.Delivered && demand.Status != DemandStatus.Approved && demand.Status != DemandStatus.PartiallyApproved)
        {
            return ApiResponse<DemandDto>.Fail("Sadece onaylanmış veya teslim edilmiş talepler teslim alınabilir.");
        }

        demand.Status = DemandStatus.Received;
        demand.ReceivedAt = DateTime.UtcNow;
        demand.ReceivedByUserId = dto.ReceivedByUserId;

        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var item in demand.Items)
        {
            // Add to DailyStockSummary
            var approvedQty = item.ApprovedQuantity ?? item.RequestedQuantity; // If not explicitly approved but Delivered (e.g. auto approved)
            
            // Only add if it was approved
            if (item.Status == DemandItemStatus.Rejected)
                continue;

            var summary = await _context.DailyStockSummaries
                .FirstOrDefaultAsync(s => s.BranchId == demand.SalesBranchId && s.ProductId == item.ProductId && s.Date == today);

            if (summary == null)
            {
                // Find carry over from previous day
                var previousSummary = await _context.DailyStockSummaries
                    .Where(s => s.BranchId == demand.SalesBranchId && s.ProductId == item.ProductId && s.Date < today && s.IsClosed)
                    .OrderByDescending(s => s.Date)
                    .FirstOrDefaultAsync();

                summary = new DailyStockSummary
                {
                    BranchId = demand.SalesBranchId,
                    ProductId = item.ProductId,
                    Date = today,
                    OpeningStock = previousSummary?.CarryOverQuantity ?? 0,
                    ReceivedFromDemands = approvedQty
                };
                _context.DailyStockSummaries.Add(summary);
            }
            else
            {
                summary.ReceivedFromDemands += approvedQty;
            }
        }

        await _context.SaveChangesAsync();

        return await GetDemandByIdAsync(demand.Id);
    }
}
