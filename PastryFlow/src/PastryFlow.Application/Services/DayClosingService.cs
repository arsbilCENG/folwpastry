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

            // KURAL 1: Counter Ürünler Stock Kaydı ALMAZ
            if (detail.Product.TrackingType != TrackingType.Counter)
            {
                var stock = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.BranchId == branchId && s.ProductId == detail.ProductId);

                if (stock == null)
                {
                    stock = new Stock
                    {
                        BranchId = branchId,
                        ProductId = detail.ProductId,
                        CurrentQuantity = detail.CarryOverQuantity
                    };
                    _context.Stocks.Add(stock);
                }
                else
                {
                    stock.CurrentQuantity = detail.CarryOverQuantity;
                    stock.UpdatedAt = DateTime.UtcNow;
                }
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

    public async Task<ApiResponse<ExpectedCashDto>> CalculateExpectedCashAsync(Guid id)
    {
        var dayClosing = await _context.DayClosings
            .Include(dc => dc.Details).ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(dc => dc.Id == id);

        if (dayClosing == null) return ApiResponse<ExpectedCashDto>.Fail("Gün kaydı bulunamadı.");

        // 1. Satış geliri
        decimal totalSalesRevenue = 0;
        var response = new ExpectedCashDto
        {
            OpeningCashBalance = dayClosing.OpeningCashBalance,
            Items = new List<ExpectedCashItemDto>()
        };

        foreach (var detail in dayClosing.Details)
        {
            if (detail.Product == null) continue;

            var calculatedSales = (detail.OpeningStock + detail.ReceivedFromDemands + detail.IncomingTransferQuantity)
                                  - (detail.OutgoingTransferQuantity + detail.EndOfDayCount + detail.DayWasteQuantity);

            if (detail.Product.UnitPrice.HasValue)
            {
                var salesValue = calculatedSales * detail.Product.UnitPrice.Value;
                totalSalesRevenue += salesValue;
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

        response.TotalSalesRevenue = totalSalesRevenue;

        // 2. Bugünkü nakit satın alımlar
        var targetDate = DateTime.SpecifyKind(dayClosing.Date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var nextDate = targetDate.AddDays(1);

        var cashPurchases = await _context.Purchases
            .Where(p => p.BranchId == dayClosing.BranchId
                     && p.PurchaseDate >= targetDate && p.PurchaseDate < nextDate
                     && p.PaymentMethod == PaymentMethod.Cash)
            .SumAsync(p => p.TotalAmount);

        // 3. Bugünkü admin nakit çekimleri
        var cashWithdrawals = await _context.CashTransactions
            .Where(t => t.BranchId == dayClosing.BranchId
                     && t.TransactionDate >= targetDate && t.TransactionDate < nextDate
                     && t.TransactionType == TransactionType.AdminWithdrawal
                     && t.Method == PaymentMethod.Cash)
            .SumAsync(t => t.Amount);

        // 4. Bugünkü admin nakit yatırımları
        var cashDeposits = await _context.CashTransactions
            .Where(t => t.BranchId == dayClosing.BranchId
                     && t.TransactionDate >= targetDate && t.TransactionDate < nextDate
                     && t.TransactionType == TransactionType.AdminDeposit
                     && t.Method == PaymentMethod.Cash)
            .SumAsync(t => t.Amount);

        // Beklenen nakit = Açılış + (Gelir - POS) + Yatırım - Satın Alım - Çekim
        var posAmount = dayClosing.PosAmount ?? 0;
        var expectedCash = dayClosing.OpeningCashBalance
            + (totalSalesRevenue - posAmount)
            + cashDeposits
            - cashPurchases
            - cashWithdrawals;

        response.CashPurchases = cashPurchases;
        response.CashWithdrawals = cashWithdrawals;
        response.CashDeposits = cashDeposits;
        response.ExpectedCashAmount = Math.Max(0, expectedCash);
        response.ExpectedAmount = totalSalesRevenue; // Backward compatibility if needed

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

        var expectedCashRes = await CalculateExpectedCashAsync(closing.Id);
        if (!expectedCashRes.Success || expectedCashRes.Data == null)
            return ApiResponse<DayClosingSummaryDto>.Fail("Beklenen tutar hesaplanamadı.");

        var expectedAmount = expectedCashRes.Data.ExpectedCashAmount; // YENİ: Tam kasa denklemi sonucu
        var totalCounted = dto.CashAmount + dto.PosAmount;
        var diff = dto.CashAmount - expectedAmount; // YENİ: Nakit farkı

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
                .Where(d => d.Product != null && d.Product.IsActive)
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
            // Bir önceki günün kapanış nakitini bul
            var previousClosing = await _context.DayClosings
                .Where(dc => dc.BranchId == branchId
                          && dc.IsClosed
                          && dc.Date < date)
                .OrderByDescending(dc => dc.Date)
                .FirstOrDefaultAsync();

            var openingCash = previousClosing?.CashAmount ?? 0;

            closing = new DayClosing
            {
                BranchId = branchId,
                Date = date,
                IsOpened = true,
                OpenedAt = DateTime.UtcNow,
                OpeningCashBalance = openingCash
            };
            _context.DayClosings.Add(closing);
            await _context.SaveChangesAsync();
        }

        return closing;
    }
}
