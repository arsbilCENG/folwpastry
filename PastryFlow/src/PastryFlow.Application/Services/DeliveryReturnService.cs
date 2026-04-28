using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.DeliveryReturns;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryFlow.Application.Services;

public class DeliveryReturnService : IDeliveryReturnService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;

    public DeliveryReturnService(IPastryFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<DeliveryReturnDto>>> GetReturnsAsync(Guid? branchId, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.DeliveryReturns
            .Include(r => r.Product)
            .ThenInclude(p => p.Category)
            .Include(r => r.FromBranch)
            .Include(r => r.ToBranch)
            .AsQueryable();

        if (branchId.HasValue)
        {
            query = query.Where(r => r.FromBranchId == branchId.Value || r.ToBranchId == branchId.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(r => r.CreatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(r => r.CreatedAt <= endDate.Value);
        }

        var returns = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();
        return ApiResponse<List<DeliveryReturnDto>>.Ok(_mapper.Map<List<DeliveryReturnDto>>(returns));
    }

    public async Task<ApiResponse<List<DeliveryReturnDto>>> GetReturnsByDemandAsync(Guid demandId)
    {
        var returns = await _context.DeliveryReturns
            .Include(r => r.Product)
            .ThenInclude(p => p.Category)
            .Include(r => r.FromBranch)
            .Include(r => r.ToBranch)
            .Where(r => r.DemandId == demandId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return ApiResponse<List<DeliveryReturnDto>>.Ok(_mapper.Map<List<DeliveryReturnDto>>(returns));
    }

    public async Task<ApiResponse<bool>> AcknowledgeReturnAsync(Guid returnId, Guid userId)
    {
        var record = await _context.DeliveryReturns.FindAsync(returnId);
        if (record == null) return ApiResponse<bool>.Fail("İade kaydı bulunamadı.");

        record.Status = DeliveryReturnStatus.Acknowledged;
        await _context.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "İade onaylandı.");
    }
}
