using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Stock;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

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
            .Where(p => p.IsActive && p.TrackingType != TrackingType.Counter)
            .OrderBy(p => p.Category.SortOrder)
            .ThenBy(p => p.SortOrder)
            .ToListAsync();

        var stocks = await _context.Stocks
            .Where(s => s.BranchId == branchId)
            .ToListAsync();

        var summaries = await _context.DayClosingDetails
            .Include(s => s.DayClosing)
            .Where(s => s.DayClosing.BranchId == branchId && s.DayClosing.Date == date)
            .ToListAsync();

        var result = products.Select(p => {
            var stock = stocks.FirstOrDefault(s => s.ProductId == p.Id);
            var summary = summaries.FirstOrDefault(x => x.ProductId == p.Id);
            
            return new CurrentStockDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CategoryName = p.Category.Name,
                Unit = p.Unit,
                OpeningStock = summary?.OpeningStock ?? 0,
                ReceivedFromDemands = summary?.ReceivedFromDemands ?? 0,
                IncomingTransfer = summary?.IncomingTransferQuantity ?? 0,
                OutgoingTransfer = summary?.OutgoingTransferQuantity ?? 0,
                DayWaste = summary?.DayWasteQuantity ?? 0,
                CurrentStock = stock?.CurrentQuantity ?? 0
            };
        }).ToList();

        return ApiResponse<List<CurrentStockDto>>.Ok(result);
    }
}
