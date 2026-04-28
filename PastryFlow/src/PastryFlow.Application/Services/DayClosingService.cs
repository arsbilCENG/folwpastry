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

        // Validations
        if (closing.CashAmount == null || closing.PosAmount == null)
            return ApiResponse<DayClosingSummaryDto>.Fail("Kasa sayımı yapılmadan gün kapatılamaz.");

        if (string.IsNullOrEmpty(closing.ReceiptPhotoUrl))
            return ApiResponse<DayClosingSummaryDto>.Fail("Gün sonu fişi fotoğrafı yüklenmelidir.");

        if (string.IsNullOrEmpty(closing.CounterPhotoUrl))
            return ApiResponse<DayClosingSummaryDto>.Fail("Tezgah fotoğrafı yüklenmelidir.");

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

    public async Task<ApiResponse<ExpectedCashDto>> CalculateExpectedCashAsync(Guid branchId, DateOnly date)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(c => c.BranchId == branchId && c.Date == date);

        if (closing == null) return ApiResponse<ExpectedCashDto>.Fail("Gün sonu kaydı bulunamadı.");

        var response = new ExpectedCashDto();

        foreach (var detail in closing.Details)
        {
            var calculatedSales = (detail.OpeningStock + detail.ReceivedFromDemands + detail.IncomingTransferQuantity)
                                  - (detail.OutgoingTransferQuantity + detail.EndOfDayCount + detail.DayWasteQuantity);

            if (detail.Product.UnitPrice.HasValue)
            {
                var salesValue = calculatedSales * detail.Product.UnitPrice.Value;
                response.ExpectedAmount += salesValue;
                response.ProductsWithPrice++;

                response.Items.Add(new ExpectedCashItemDto
                {
                    ProductName = detail.Product.Name,
                    CalculatedSales = calculatedSales,
                    UnitPrice = detail.Product.UnitPrice.Value,
                    SalesValue = salesValue
                });
            }
            else
            {
                response.ProductsWithoutPrice++;
                response.Items.Add(new ExpectedCashItemDto
                {
                    ProductName = detail.Product.Name,
                    CalculatedSales = calculatedSales,
                    UnitPrice = null,
                    SalesValue = null
                });
            }
        }

        return ApiResponse<ExpectedCashDto>.Ok(response);
    }

    public async Task<ApiResponse<DayClosingSummaryDto>> SubmitCashCountAsync(Guid dayClosingId, CashCountDto dto, Guid currentUserId)
    {
        var closing = await _context.DayClosings
            .Include(c => c.Details)
            .FirstOrDefaultAsync(c => c.Id == dayClosingId);

        if (closing == null) return ApiResponse<DayClosingSummaryDto>.Fail("Kayıt bulunamadı.");
        if (closing.IsClosed) return ApiResponse<DayClosingSummaryDto>.Fail("Bu gün zaten kapatılmış.");

        if (!closing.Details.Any(d => d.EndOfDayCount > 0))
            return ApiResponse<DayClosingSummaryDto>.Fail("Önce ürün sayımını tamamlayınız.");

        var expectedCashRes = await CalculateExpectedCashAsync(closing.BranchId, closing.Date);
        if (!expectedCashRes.Success || expectedCashRes.Data == null)
            return ApiResponse<DayClosingSummaryDto>.Fail("Beklenen tutar hesaplanamadı.");

        var expectedAmount = expectedCashRes.Data.ExpectedAmount;
        var totalCounted = dto.CashAmount + dto.PosAmount;
        var diff = totalCounted - expectedAmount;

        if (diff != 0 && string.IsNullOrWhiteSpace(dto.DifferenceNote))
            return ApiResponse<DayClosingSummaryDto>.Fail("Kasa farkı bulunmaktadır. Lütfen açıklama giriniz.");

        closing.ExpectedCashAmount = expectedAmount;
        closing.CashAmount = dto.CashAmount;
        closing.PosAmount = dto.PosAmount;
        closing.TotalCounted = totalCounted;
        closing.CashDifference = diff;
        closing.DifferenceNote = dto.DifferenceNote;

        await _context.SaveChangesAsync();
        return await GetSummaryAsync(closing.BranchId, closing.Date);
    }

    public async Task<ApiResponse<string>> UpdateReceiptPhotoAsync(Guid dayClosingId, string photoUrl)
    {
        var closing = await _context.DayClosings.FindAsync(dayClosingId);
        if (closing == null) return ApiResponse<string>.Fail("Kayıt bulunamadı.");
        
        closing.ReceiptPhotoUrl = photoUrl;
        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok(photoUrl, "Fiş fotoğrafı yüklendi.");
    }

    public async Task<ApiResponse<string>> UpdateCounterPhotoAsync(Guid dayClosingId, string photoUrl)
    {
        var closing = await _context.DayClosings.FindAsync(dayClosingId);
        if (closing == null) return ApiResponse<string>.Fail("Kayıt bulunamadı.");
        
        closing.CounterPhotoUrl = photoUrl;
        await _context.SaveChangesAsync();
        return ApiResponse<string>.Ok(photoUrl, "Tezgah fotoğrafı yüklendi.");
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
            Id = closing.Id,
            BranchName = branch.Name,
            Date = date,
            IsClosed = closing.IsClosed,
            ExpectedCashAmount = closing.ExpectedCashAmount,
            CashAmount = closing.CashAmount,
            PosAmount = closing.PosAmount,
            TotalCounted = closing.TotalCounted,
            CashDifference = closing.CashDifference,
            DifferenceNote = closing.DifferenceNote,
            ReceiptPhotoUrl = closing.ReceiptPhotoUrl,
            CounterPhotoUrl = closing.CounterPhotoUrl,
            Items = closing.Details
                .Where(d => d.Product.IsActive)
                .OrderBy(d => d.Product.Category.SortOrder)
                .ThenBy(d => d.Product.Name)
                .Select(d => new DailySummaryItemDto
                {
                    Id = d.Id,
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
                        d.CalculatedSales,
                    IsCorrected = d.OriginalEndOfDayCount.HasValue,
                    OriginalEndOfDayCount = d.OriginalEndOfDayCount,
                    OriginalCarryOverQuantity = d.OriginalCarryOverQuantity,
                    LastCorrectionReason = d.CorrectionReason
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
