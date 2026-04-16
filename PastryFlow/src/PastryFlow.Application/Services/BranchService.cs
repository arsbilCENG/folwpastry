using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Branch;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class BranchService : IBranchService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IMapper _mapper;

    public BranchService(IPastryFlowDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<BranchDto>>> GetBranchesAsync(BranchType? type = null)
    {
        var query = _context.Branches.Where(b => b.IsActive);
        
        if (type.HasValue)
        {
            query = query.Where(b => b.BranchType == type.Value);
        }

        var branches = await query.ToListAsync();
        return ApiResponse<List<BranchDto>>.Ok(_mapper.Map<List<BranchDto>>(branches));
    }

    public async Task<ApiResponse<BranchDto>> GetBranchByIdAsync(Guid id)
    {
        var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Id == id && b.IsActive);
        if (branch == null)
            return ApiResponse<BranchDto>.Fail("Şube bulunamadı.");

        return ApiResponse<BranchDto>.Ok(_mapper.Map<BranchDto>(branch));
    }
}
