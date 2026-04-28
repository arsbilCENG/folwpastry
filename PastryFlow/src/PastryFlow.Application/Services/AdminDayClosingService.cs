using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.DTOs.DayClosing;
using PastryFlow.Application.DTOs.Notifications;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class AdminDayClosingService : IAdminDayClosingService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IDayClosingService _dayClosingService;
    private readonly INotificationService _notificationService;

    public AdminDayClosingService(
        IPastryFlowDbContext context, 
        IDayClosingService dayClosingService,
        INotificationService notificationService)
    {
        _context = context;
        _dayClosingService = dayClosingService;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> GetBranchDayClosingAsync(Guid branchId, DateOnly date)
    {
        var response = await _dayClosingService.GetSummaryAsync(branchId, date);
        if (!response.Success || response.Data == null) return response;
        return response;
    }

    public async Task<ApiResponse<DailySummaryItemDto>> CorrectDayClosingDetailAsync(Guid dayClosingId, DayClosingCorrectionDto dto, Guid currentUserId)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .Include(c => c.Branch)
            .FirstOrDefaultAsync(c => c.Id == dayClosingId);

        if (closing == null) return ApiResponse<DailySummaryItemDto>.Fail("Gün kapanış kaydı bulunamadı.");

        var detail = closing.Details.FirstOrDefault(d => d.Id == dto.DayClosingDetailId);
        if (detail == null) return ApiResponse<DailySummaryItemDto>.Fail("Kapanış detayı bulunamadı.");

        if (dto.CorrectedCarryOverQuantity > dto.CorrectedEndOfDayCount)
            return ApiResponse<DailySummaryItemDto>.Fail("Devir miktarı gün sonu sayımından büyük olamaz.");

        if (detail.OriginalEndOfDayCount == null)
        {
            detail.OriginalEndOfDayCount = detail.EndOfDayCount;
            detail.OriginalCarryOverQuantity = detail.CarryOverQuantity;
        }

        detail.EndOfDayCount = dto.CorrectedEndOfDayCount;
        detail.CarryOverQuantity = dto.CorrectedCarryOverQuantity;
        
        detail.CorrectedEndOfDayCount = dto.CorrectedEndOfDayCount;
        detail.CorrectedCarryOverQuantity = dto.CorrectedCarryOverQuantity;
        detail.CorrectionReason = dto.CorrectionReason;
        detail.CorrectedAt = DateTime.UtcNow;
        detail.CorrectedByUserId = currentUserId;

        detail.CalculatedSales = (detail.OpeningStock + detail.ReceivedFromDemands + detail.IncomingTransferQuantity)
                                 - (detail.OutgoingTransferQuantity + detail.EndOfDayCount + detail.DayWasteQuantity);
        
        detail.EndOfDayWaste = detail.EndOfDayCount - detail.CarryOverQuantity;

        await _context.SaveChangesAsync();

        // Notification: Gün sonu düzeltildiğinde
        try
        {
            await _notificationService.CreateAndSendAsync(new CreateNotificationDto
            {
                BranchId = closing.BranchId,
                Title = "Gün Sonu Düzeltmesi",
                Message = $"Gün sonu sayımınız admin tarafından düzeltildi. Sebep: {dto.CorrectionReason}",
                Type = NotificationType.DayClosingCorrected,
                SourceEntity = "DayClosing",
                SourceEntityId = closing.Id,
                SourceBranchId = closing.BranchId,
                SourceBranchName = closing.Branch?.Name
            });
        }
        catch (Exception) { }

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
