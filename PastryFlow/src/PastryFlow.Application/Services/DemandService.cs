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
                // If quantity is reduced, keep the reason (if provided)
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

        // Calculate overall demand status
        bool allFullApproved = demand.Items.All(i => i.Status == DemandItemStatus.Approved && i.ApprovedQuantity == i.RequestedQuantity);
        bool allRejected = demand.Items.All(i => i.Status == DemandItemStatus.Rejected);

        demand.Status = allFullApproved ? DemandStatus.Approved :
                        allRejected ? DemandStatus.Rejected :
                        DemandStatus.PartiallyApproved;

        demand.ReviewedByUserId = dto.ReviewedByUserId;
        demand.ReviewedAt = reviewedAt;

        await _context.SaveChangesAsync();

        // Create notification for sales branch
        var statusText = demand.Status == DemandStatus.Approved ? "onaylandı" :
                         demand.Status == DemandStatus.Rejected ? "reddedildi" :
                         "kısmen onaylandı";
        await _notificationService.CreateAsync(
            userId: null,
            branchId: demand.SalesBranchId,
            title: "Talep Güncellendi",
            message: $"Talebiniz {demand.DemandNumber} {statusText}.",
            relatedEntityType: "Demand",
            relatedEntityId: demand.Id
        );

        // If any rejected, notify admin
        var rejectedCount = demand.Items.Count(i => i.Status == DemandItemStatus.Rejected);
        if (rejectedCount > 0)
        {
            await _notificationService.CreateAsync(
                userId: null,
                branchId: null,
                title: "Talep Kalemler Reddedildi",
                message: $"{demand.SalesBranch.Name} talebi {demand.DemandNumber}: {rejectedCount} kalem reddedildi.",
                relatedEntityType: "Demand",
                relatedEntityId: demand.Id
            );
        }

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<DemandDto>> DeliverDemandAsync(Guid id, DeliverDemandDto dto)
    {
        var demand = await _context.Demands
            .FirstOrDefaultAsync(d => d.Id == id);

        if (demand == null)
            return ApiResponse<DemandDto>.Fail("Talep bulunamadı.");

        if (demand.Status != DemandStatus.Approved && demand.Status != DemandStatus.PartiallyApproved)
            return ApiResponse<DemandDto>.Fail("Sadece onaylanmış talepler şoföre teslim edilebilir.");

        demand.Status = DemandStatus.Delivered;
        demand.DeliveredAt = DateTime.UtcNow;
        demand.DriverUserId = dto.DriverUserId;

        await _context.SaveChangesAsync();

        return await GetDemandByIdAsync(demand.Id);
    }

    public async Task<ApiResponse<DemandDto?>> GetLastDemandAsync(Guid salesBranchId, Guid productionBranchId)
    {
        // Get yesterday's date
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
            var approvedQty = item.ApprovedQuantity ?? item.RequestedQuantity;
            
            if (item.Status == DemandItemStatus.Rejected)
                continue;

            var summary = await _context.DailyStockSummaries
                .FirstOrDefaultAsync(s => s.BranchId == demand.SalesBranchId && s.ProductId == item.ProductId && s.Date == today);

            if (summary == null)
            {
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
