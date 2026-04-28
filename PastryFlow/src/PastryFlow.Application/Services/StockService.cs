using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Stock;
using PastryFlow.Application.Interfaces;

namespace PastryFlow.Application.Services;

public class StockService : IStockService
{
    private readonly IPastryFlowDbContext _context;

    public StockService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<CurrentStockDto>>> GetCurrentStockAsync(Guid branchId, DateOnly date)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Category.SortOrder)
            .ThenBy(p => p.Name)
            .ToListAsync();

        var summaries = await _context.DayClosingDetails
            .Include(s => s.DayClosing)
            .Where(s => s.DayClosing.BranchId == branchId && s.DayClosing.Date == date)
            .ToListAsync();

        var result = products.Select(p => {
            var s = summaries.FirstOrDefault(x => x.ProductId == p.Id);
            return new CurrentStockDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CategoryName = p.Category.Name,
                Unit = p.Unit,
                OpeningStock = s?.OpeningStock ?? 0,
                ReceivedFromDemands = s?.ReceivedFromDemands ?? 0,
                IncomingTransfer = s?.IncomingTransferQuantity ?? 0,
                OutgoingTransfer = s?.OutgoingTransferQuantity ?? 0,
                DayWaste = s?.DayWasteQuantity ?? 0,
                CurrentStock = (s?.OpeningStock ?? 0) + (s?.ReceivedFromDemands ?? 0) + (s?.IncomingTransferQuantity ?? 0) - (s?.OutgoingTransferQuantity ?? 0) - (s?.DayWasteQuantity ?? 0)
            };
        }).ToList();

        return ApiResponse<List<CurrentStockDto>>.Ok(result);
    }
}
