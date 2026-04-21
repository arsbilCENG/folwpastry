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
        var summaries = await _context.DayClosingDetails
            .Include(s => s.Product)
            .Include(s => s.Product.Category)
            .Include(s => s.DayClosing)
            .Where(s => s.DayClosing.BranchId == branchId && s.DayClosing.Date == date && s.Product.IsActive)
            .OrderBy(s => s.Product.Category.SortOrder)
            .ThenBy(s => s.Product.Name)
            .ToListAsync();

        var result = summaries.Select(s => new CurrentStockDto
        {
            ProductId = s.ProductId,
            ProductName = s.Product.Name,
            CategoryName = s.Product.Category.Name,
            Unit = s.Product.Unit,
            OpeningStock = s.OpeningStock,
            ReceivedFromDemands = s.ReceivedFromDemands,
            IncomingTransfer = s.IncomingTransferQuantity,
            OutgoingTransfer = s.OutgoingTransferQuantity,
            DayWaste = s.DayWasteQuantity,
            CurrentStock = s.OpeningStock + s.ReceivedFromDemands + s.IncomingTransferQuantity - s.OutgoingTransferQuantity - s.DayWasteQuantity
        }).ToList();

        return ApiResponse<List<CurrentStockDto>>.Ok(result);
    }
}
