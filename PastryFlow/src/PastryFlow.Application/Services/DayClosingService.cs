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
        var closing = await GetOrCreateDayClosingAsync(dto.BranchId, dto.Date);
        if (closing.IsClosed) return ApiResponse<string>.Fail("Bu gün zaten kapatılmış.");

        foreach (var item in dto.Items)
        {
            var detail = closing.Details.FirstOrDefault(d => d.ProductId == item.ProductId);
            if (detail == null)
            {
                detail = new DayClosingDetail
                {
                    DayClosingId = closing.Id,
                    ProductId = item.ProductId,
                    EndOfDayCount = item.EndOfDayCount
                };
                _context.DayClosingDetails.Add(detail);
            }
            else
            {
                detail.EndOfDayCount = item.EndOfDayCount;
            }
        }

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Sayım sonuçları kaydedildi.");
    }

    public async Task<ApiResponse<string>> SaveCarryOverAsync(CarryOverInputDto dto)
    {
        var closing = await GetOrCreateDayClosingAsync(dto.BranchId, dto.Date);
        if (closing.IsClosed) return ApiResponse<string>.Fail("Bu gün zaten kapatılmış.");

        foreach (var item in dto.Items)
        {
            var detail = closing.Details.FirstOrDefault(d => d.ProductId == item.ProductId);
            if (detail != null)
            {
                detail.CarryOverQuantity = item.CarryOverQuantity;
                detail.EndOfDayWaste = detail.EndOfDayCount - detail.CarryOverQuantity;
            }
        }

        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok("Devir miktarları kaydedildi.");
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> CloseDayAsync(Guid branchId, DateOnly date, Guid closedByUserId)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .ThenInclude(d => d.Product)
            .Include(c => c.Details)
            .ThenInclude(d => d.Product.Category)
            .FirstOrDefaultAsync(c => c.BranchId == branchId && c.Date == date);

        if (closing == null) return ApiResponse<DayClosingSummaryDto>.Fail("Kapatılacak veri bulunamadı.");
        if (closing.IsClosed) return ApiResponse<DayClosingSummaryDto>.Fail("Bu gün zaten kapatılmış.");

        closing.IsClosed = true;
        closing.ClosedByUserId = closedByUserId;
        closing.ClosedAt = DateTime.UtcNow;

        var nextDay = date.AddDays(1);
        var nextClosing = await GetOrCreateDayClosingAsync(branchId, nextDay);

        foreach (var detail in closing.Details)
        {
            // CalculatedSales = (Opening + ReceivedFromDemands + IncomingTransfer) - (OutgoingTransfer + EndOfDayCount + DayWaste)
            detail.CalculatedSales = (detail.OpeningStock + detail.ReceivedFromDemands + detail.IncomingTransferQuantity)
                                     - (detail.OutgoingTransferQuantity + detail.EndOfDayCount + detail.DayWasteQuantity);

            // Carry Over to next day
            var nextDetail = nextClosing.Details.FirstOrDefault(d => d.ProductId == detail.ProductId);
            if (nextDetail == null)
            {
                nextDetail = new DayClosingDetail
                {
                    DayClosingId = nextClosing.Id,
                    ProductId = detail.ProductId,
                    OpeningStock = detail.CarryOverQuantity
                };
                _context.DayClosingDetails.Add(nextDetail);
            }
            else
            {
                nextDetail.OpeningStock = detail.CarryOverQuantity;
            }

            // Create EndOfDay Waste Records if > 0
            if (detail.EndOfDayWaste > 0)
            {
                var waste = new Waste
                {
                    BranchId = branchId,
                    ProductId = detail.ProductId,
                    Date = date,
                    Quantity = detail.EndOfDayWaste,
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

        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .ThenInclude(d => d.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(c => c.BranchId == branchId && c.Date == date);

        if (closing == null)
        {
            return ApiResponse<DayClosingSummaryDto>.Ok(new DayClosingSummaryDto
            {
                BranchName = branch.Name,
                Date = date,
                IsClosed = false,
                Items = new List<DailySummaryItemDto>()
            });
        }

        var response = new DayClosingSummaryDto
        {
            BranchName = branch.Name,
            Date = date,
            IsClosed = closing.IsClosed,
            Items = closing.Details
                .Where(d => d.Product.IsActive)
                .OrderBy(d => d.Product.Category.SortOrder)
                .ThenBy(d => d.Product.Name)
                .Select(d => new DailySummaryItemDto
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    CategoryName = d.Product.Category.Name,
                    Unit = d.Product.Unit.ToString(),
                    OpeningStock = d.OpeningStock,
                    ReceivedFromDemands = d.ReceivedFromDemands,
                    IncomingTransfer = d.IncomingTransferQuantity,
                    OutgoingTransfer = d.OutgoingTransferQuantity,
                    DayWaste = d.DayWasteQuantity,
                    EndOfDayCount = d.EndOfDayCount,
                    CarryOver = d.CarryOverQuantity,
                    EndOfDayWaste = d.EndOfDayWaste,
                    CalculatedSales = d.CalculatedSales == 0 && !closing.IsClosed ? 
                        ((d.OpeningStock + d.ReceivedFromDemands + d.IncomingTransferQuantity) - (d.OutgoingTransferQuantity + d.EndOfDayCount + d.DayWasteQuantity)) : 
                        d.CalculatedSales 
                }).ToList()
        };

        response.Totals.TotalSales = response.Items.Sum(i => i.CalculatedSales);
        response.Totals.TotalWaste = response.Items.Sum(i => i.DayWaste + i.EndOfDayWaste);
        response.Totals.TotalCarryOver = response.Items.Sum(i => i.CarryOver);

        return ApiResponse<DayClosingSummaryDto>.Ok(response);
    }

    private async Task<DayClosing> GetOrCreateDayClosingAsync(Guid branchId, DateOnly date)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .FirstOrDefaultAsync(c => c.BranchId == branchId && c.Date == date);

        if (closing == null)
        {
            closing = new DayClosing
            {
                BranchId = branchId,
                Date = date,
                IsOpened = true,
                OpenedAt = DateTime.UtcNow
            };
            _context.DayClosings.Add(closing);
            await _context.SaveChangesAsync();
        }

        return closing;
    }
}
