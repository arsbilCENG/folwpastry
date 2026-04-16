using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.DayClosing;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class DayClosingService : IDayClosingService
{
    private readonly IPastryFlowDbContext _context;

    public DayClosingService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<string>> SaveCountAsync(CountInputDto dto)
    {
        // Upsert DailyStockSummaries for EndOfDayCount
        foreach (var item in dto.Items)
        {
            var summary = await _context.DailyStockSummaries
                .FirstOrDefaultAsync(s => s.BranchId == dto.BranchId && s.Date == dto.Date && s.ProductId == item.ProductId);

            if (summary == null)
            {
                // Create new
                summary = new DailyStockSummary
                {
                    BranchId = dto.BranchId,
                    Date = dto.Date,
                    ProductId = item.ProductId,
                    EndOfDayCount = item.EndOfDayCount
                };
                _context.DailyStockSummaries.Add(summary);
            }
            else
            {
                if (summary.IsClosed) continue; // cannot mutate closed day
                summary.EndOfDayCount = item.EndOfDayCount;
            }
        }
        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Sayım sonuçları kaydedildi.");
    }

    public async Task<ApiResponse<string>> SaveCarryOverAsync(CarryOverInputDto dto)
    {
        foreach (var item in dto.Items)
        {
            var summary = await _context.DailyStockSummaries
                .FirstOrDefaultAsync(s => s.BranchId == dto.BranchId && s.Date == dto.Date && s.ProductId == item.ProductId);

            if (summary != null && !summary.IsClosed)
            {
                summary.CarryOverQuantity = item.CarryOverQuantity;
                summary.EndOfDayWaste = summary.EndOfDayCount - summary.CarryOverQuantity;
            }
        }
        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Devir miktarları kaydedildi.");
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> CloseDayAsync(Guid branchId, DateOnly date, Guid closedByUserId)
    {
        var summaries = await _context.DailyStockSummaries
            .Include(s => s.Product)
            .Where(s => s.BranchId == branchId && s.Date == date)
            .ToListAsync();

        if (summaries.Any(s => s.IsClosed))
        {
            return ApiResponse<DayClosingSummaryDto>.Fail("Bu gün zaten kapatılmış.");
        }

        var nextDay = date.AddDays(1);

        foreach (var summary in summaries)
        {
            // CalculatedSales = (Opening + ReceivedFromDemands + IncomingTransfer) - (OutgoingTransfer + EndOfDayCount + DayWaste)
            summary.CalculatedSales = (summary.OpeningStock + summary.ReceivedFromDemands + summary.IncomingTransferQuantity)
                                      - (summary.OutgoingTransferQuantity + summary.EndOfDayCount + summary.DayWasteQuantity);
            
            summary.IsClosed = true;
            summary.ClosedByUserId = closedByUserId;
            summary.ClosedAt = DateTime.UtcNow;

            // Create Next Day's Summary
            var nextSummary = new DailyStockSummary
            {
                BranchId = branchId,
                ProductId = summary.ProductId,
                Date = nextDay,
                OpeningStock = summary.CarryOverQuantity
            };
            _context.DailyStockSummaries.Add(nextSummary);

            // Create EndOfDay Waste Records if > 0
            if (summary.EndOfDayWaste > 0)
            {
                var waste = new Waste
                {
                    BranchId = branchId,
                    ProductId = summary.ProductId,
                    Date = date,
                    Quantity = summary.EndOfDayWaste,
                    WasteType = WasteType.EndOfDay,
                    CreatedByUserId = closedByUserId,
                    Notes = "Gün sonu otomatik atık"
                };
                _context.Wastes.Add(waste);
            }
        }

        await _context.SaveChangesAsync();

        return await GetSummaryAsync(branchId, date);
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> GetSummaryAsync(Guid branchId, DateOnly date)
    {
        var branch = await _context.Branches.FindAsync(branchId);
        if (branch == null) return ApiResponse<DayClosingSummaryDto>.Fail("Şube bulunamadı.");

        var summaries = await _context.DailyStockSummaries
            .Include(s => s.Product)
            .Include(s => s.Product.Category)
            .Where(s => s.BranchId == branchId && s.Date == date && s.Product.IsActive)
            .OrderBy(s => s.Product.Category.SortOrder)
            .ThenBy(s => s.Product.Name)
            .ToListAsync();

        var isClosed = summaries.Any() && summaries.All(s => s.IsClosed);

        var response = new DayClosingSummaryDto
        {
            BranchName = branch.Name,
            Date = date,
            IsClosed = isClosed,
            Items = summaries.Select(s => new DailySummaryItemDto
            {
                ProductId = s.ProductId,
                ProductName = s.Product.Name,
                CategoryName = s.Product.Category.Name,
                Unit = s.Product.Unit.ToString(),
                OpeningStock = s.OpeningStock,
                ReceivedFromDemands = s.ReceivedFromDemands,
                IncomingTransfer = s.IncomingTransferQuantity,
                OutgoingTransfer = s.OutgoingTransferQuantity,
                DayWaste = s.DayWasteQuantity,
                EndOfDayCount = s.EndOfDayCount,
                CarryOver = s.CarryOverQuantity,
                EndOfDayWaste = s.EndOfDayWaste,
                CalculatedSales = s.CalculatedSales == 0 && !s.IsClosed ? 
                    ((s.OpeningStock + s.ReceivedFromDemands + s.IncomingTransferQuantity) - (s.OutgoingTransferQuantity + s.EndOfDayCount + s.DayWasteQuantity)) : 
                    s.CalculatedSales 
            }).ToList()
        };

        response.Totals.TotalSales = response.Items.Sum(i => i.CalculatedSales);
        response.Totals.TotalWaste = response.Items.Sum(i => i.DayWaste + i.EndOfDayWaste);
        response.Totals.TotalCarryOver = response.Items.Sum(i => i.CarryOver);

        return ApiResponse<DayClosingSummaryDto>.Ok(response);
    }
}
