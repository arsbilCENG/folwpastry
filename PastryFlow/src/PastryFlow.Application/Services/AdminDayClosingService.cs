using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.DTOs.DayClosing;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Services;

public class AdminDayClosingService : IAdminDayClosingService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IDayClosingService _dayClosingService;

    public AdminDayClosingService(IPastryFlowDbContext context, IDayClosingService dayClosingService)
    {
        _context = context;
        _dayClosingService = dayClosingService;
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> GetBranchDayClosingAsync(Guid branchId, DateOnly date)
    {
        // Admin gets more detailed summary (with correction info)
        var response = await _dayClosingService.GetSummaryAsync(branchId, date);
        if (!response.Success || response.Data == null) return response;

        // In a real scenario, we might want to attach correction info to the DTO.
        // For now, this is enough since the summary DTO already has the main fields.
        return response;
    }

    public async Task<ApiResponse<DailySummaryItemDto>> CorrectDayClosingDetailAsync(Guid dayClosingId, DayClosingCorrectionDto dto, Guid currentUserId)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .FirstOrDefaultAsync(c => c.Id == dayClosingId);

        if (closing == null) return ApiResponse<DailySummaryItemDto>.Fail("Gün kapanış kaydı bulunamadı.");

        var detail = closing.Details.FirstOrDefault(d => d.Id == dto.DayClosingDetailId);
        if (detail == null) return ApiResponse<DailySummaryItemDto>.Fail("Kapanış detayı bulunamadı.");

        if (dto.CorrectedCarryOverQuantity > dto.CorrectedEndOfDayCount)
            return ApiResponse<DailySummaryItemDto>.Fail("Devir miktarı gün sonu sayımından büyük olamaz.");

        // First time correction: capture original values
        if (detail.OriginalEndOfDayCount == null)
        {
            detail.OriginalEndOfDayCount = detail.EndOfDayCount;
            detail.OriginalCarryOverQuantity = detail.CarryOverQuantity;
        }

        // Update with corrected values
        detail.EndOfDayCount = dto.CorrectedEndOfDayCount;
        detail.CarryOverQuantity = dto.CorrectedCarryOverQuantity;
        
        detail.CorrectedEndOfDayCount = dto.CorrectedEndOfDayCount;
        detail.CorrectedCarryOverQuantity = dto.CorrectedCarryOverQuantity;
        detail.CorrectionReason = dto.CorrectionReason;
        detail.CorrectedAt = DateTime.UtcNow;
        detail.CorrectedByUserId = currentUserId;

        // Recalculate sales
        detail.CalculatedSales = (detail.OpeningStock + detail.ReceivedFromDemands + detail.IncomingTransferQuantity)
                                 - (detail.OutgoingTransferQuantity + detail.EndOfDayCount + detail.DayWasteQuantity);
        
        detail.EndOfDayWaste = detail.EndOfDayCount - detail.CarryOverQuantity;

        await _context.SaveChangesAsync();

        // Update next day's opening stock if it exists
        var nextDay = closing.Date.AddDays(1);
        var nextClosing = await _context.DayClosings
            .Include(c => c.Details)
            .FirstOrDefaultAsync(c => c.BranchId == closing.BranchId && c.Date == nextDay);

        if (nextClosing != null)
        {
            var nextDetail = nextClosing.Details.FirstOrDefault(d => d.ProductId == detail.ProductId);
            if (nextDetail != null)
            {
                nextDetail.OpeningStock = detail.CarryOverQuantity;
                await _context.SaveChangesAsync();
            }
        }

        return ApiResponse<DailySummaryItemDto>.Ok(new DailySummaryItemDto
        {
            ProductId = detail.ProductId,
            ProductName = await _context.Products.Where(p => p.Id == detail.ProductId).Select(p => p.Name).FirstOrDefaultAsync() ?? "",
            CategoryName = "", // Simplified for response
            OpeningStock = detail.OpeningStock,
            ReceivedFromDemands = detail.ReceivedFromDemands,
            IncomingTransfer = detail.IncomingTransferQuantity,
            OutgoingTransfer = detail.OutgoingTransferQuantity,
            DayWaste = detail.DayWasteQuantity,
            EndOfDayCount = detail.EndOfDayCount,
            CarryOver = detail.CarryOverQuantity,
            EndOfDayWaste = detail.EndOfDayWaste,
            CalculatedSales = detail.CalculatedSales
        }, "Düzeltme başarıyla kaydedildi.");
    }
}
