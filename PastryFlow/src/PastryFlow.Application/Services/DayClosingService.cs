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
using Microsoft.Extensions.Logging;

namespace PastryFlow.Application.Services;

public class DayClosingService : IDayClosingService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IWalletService _walletService;
    private readonly ILogger<DayClosingService> _logger;

    public DayClosingService(IPastryFlowDbContext context, IWalletService walletService, ILogger<DayClosingService> logger)
    {
        _context = context;
        _walletService = walletService;
        _logger = logger;
    }

    public async Task<ApiResponse<string>> SaveCountAsync(CountInputDto dto)
    {
        if (!DateOnly.TryParse(dto.Date, out var date))
            return ApiResponse<string>.Fail("Geçersiz tarih formatı.");

        var closing = await GetOrCreateDayClosingAsync(dto.BranchId, date);
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
        _logger.LogInformation("SaveCarryOverAsync called for BranchId: {BranchId}, DateString: {Date}", dto.BranchId, dto.Date);
        
        if (!DateOnly.TryParse(dto.Date, out var date))
        {
            _logger.LogWarning("Invalid date format: {Date}", dto.Date);
            return ApiResponse<string>.Fail("Geçersiz tarih formatı.");
        }

        var closing = await GetOrCreateDayClosingAsync(dto.BranchId, date);
        
        if (closing.IsClosed) 
        {
            _logger.LogWarning("DayClosing already closed for BranchId: {BranchId}, Date: {Date}", dto.BranchId, date);
            return ApiResponse<string>.Fail("Bu gün zaten kapatılmış.");
        }

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

    public async Task<ApiResponse<DayClosingSummaryDto>> CloseDayAsync(
        Guid branchId, DateOnly date, Guid closedByUserId,
        List<DayClosingCounterItemDto>? counterItems = null)
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

        // ADIM 1: Stock.CurrentQuantity'yi ÖNCE oku (güncelleme öncesi)
        var stocks = await _context.Stocks
            .Where(s => s.BranchId == branchId)
            .ToDictionaryAsync(s => s.ProductId);

        // ADIM 2: Normal ürün detayları (Production/Purchased)
        foreach (var detail in closing.Details.Where(d => d.Product.TrackingType != TrackingType.Counter))
        {
            var currentStock = stocks.GetValueOrDefault(detail.ProductId);
            var currentQty = currentStock?.CurrentQuantity ?? 0;

            var soldQty = currentQty - detail.EndOfDayCount;
            if (soldQty < 0) soldQty = 0;

            detail.CalculatedSales = soldQty;

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

        // ADIM 3: Counter ürün satış kayıtları
        decimal counterSalesTotal = 0;
        if (counterItems != null && counterItems.Count > 0)
        {
            foreach (var counterItem in counterItems)
            {
                if (counterItem.CounterSoldQuantity <= 0) continue;

                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == counterItem.ProductId);

                if (product == null || product.TrackingType != TrackingType.Counter) continue;

                var revenue = counterItem.CounterSoldQuantity * (product.UnitPrice ?? 0);
                counterSalesTotal += revenue;

                // Mevcut bir Counter detail kaydı var mı kontrol et
                var existingDetail = closing.Details.FirstOrDefault(d => d.ProductId == counterItem.ProductId);
                if (existingDetail != null)
                {
                    // Güncelle
                    existingDetail.CounterSoldQuantity = counterItem.CounterSoldQuantity;
                    existingDetail.CalculatedSales = counterItem.CounterSoldQuantity;
                }
                else
                {
                    // Yeni kayıt
                    var detail = new DayClosingDetail
                    {
                        DayClosingId = closing.Id,
                        ProductId = counterItem.ProductId,
                        OpeningStock = 0,
                        ReceivedFromDemands = 0,
                        IncomingTransferQuantity = 0,
                        OutgoingTransferQuantity = 0,
                        DayWasteQuantity = 0,
                        EndOfDayCount = 0,
                        CarryOverQuantity = 0,
                        EndOfDayWaste = 0,
                        CounterSoldQuantity = counterItem.CounterSoldQuantity,
                        CalculatedSales = counterItem.CounterSoldQuantity
                    };
                    _context.DayClosingDetails.Add(detail);
                }
            }
        }

        // ADIM 4: Stock tablosunu güncelle (hesaptan SONRA)
        foreach (var detail in closing.Details.Where(d => d.Product.TrackingType != TrackingType.Counter))
        {
            var stock = stocks.GetValueOrDefault(detail.ProductId);
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
                _context.Stocks.Update(stock);
            }
        }

        await _context.SaveChangesAsync();

        var expectedCashRes = await CalculateExpectedCashAsync(closing.Id);
        var totalSalesRevenue = expectedCashRes.Data?.TotalSalesRevenue ?? 0;
        
        var cashRevenueTotal = totalSalesRevenue - (closing.PosAmount ?? 0);
        var bankRevenueTotal = closing.PosAmount ?? 0;

        await _walletService.ApplyDayClosingRevenueAsync(
            branchId,
            cashRevenueTotal,
            bankRevenueTotal,
            closedByUserId);

        return await GetSummaryAsync(branchId, date);
    }

    public async Task<ApiResponse<ExpectedCashDto>> CalculateExpectedCashAsync(Guid id)
    {
        var dayClosing = await _context.DayClosings
            .Include(dc => dc.Details).ThenInclude(d => d.Product).ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(dc => dc.Id == id);

        if (dayClosing == null) return ApiResponse<ExpectedCashDto>.Fail("Gün kaydı bulunamadı.");

        // Production + Purchased ürünler
        var stocks = await _context.Stocks
            .Include(s => s.Product)
                .ThenInclude(p => p.Category)
            .Where(s => s.BranchId == dayClosing.BranchId
                     && s.Product.TrackingType != TrackingType.Counter
                     && s.Product.IsActive)
            .ToListAsync();

        var productItems = new List<ExpectedCashItemDto>();
        decimal productSalesTotal = 0;
        int productsWithPrice = 0;
        int productsWithoutPrice = 0;

        foreach (var stock in stocks)
        {
            var detail = dayClosing.Details.FirstOrDefault(d => d.ProductId == stock.ProductId);
            var endOfDayCount = detail?.EndOfDayCount ?? 0;

            var soldQty = stock.CurrentQuantity - endOfDayCount;
            if (soldQty < 0) soldQty = 0;

            decimal revenue = 0;
            if (stock.Product.UnitPrice.HasValue)
            {
                revenue = soldQty * stock.Product.UnitPrice.Value;
                productSalesTotal += revenue;
                productsWithPrice++;
            }
            else
            {
                productsWithoutPrice++;
            }

            productItems.Add(new ExpectedCashItemDto
            {
                ProductName = stock.Product.Name,
                CategoryName = stock.Product.Category.Name,
                CalculatedSales = soldQty,
                UnitPrice = stock.Product.UnitPrice,
                SalesValue = stock.Product.UnitPrice.HasValue ? revenue : null,
                IsCounter = false
            });
        }

        // Counter ürünler
        var counterItems = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.TrackingType == TrackingType.Counter && p.IsActive)
            .ToListAsync();

        decimal counterSalesTotal = 0;
        var counterItemDtos = new List<ExpectedCashItemDto>();

        foreach (var product in counterItems)
        {
            var detail = dayClosing.Details.FirstOrDefault(d => d.ProductId == product.Id);
            var counterSoldQty = detail?.CounterSoldQuantity ?? 0;
            
            decimal revenue = 0;
            if (product.UnitPrice.HasValue)
            {
                revenue = counterSoldQty * product.UnitPrice.Value;
                counterSalesTotal += revenue;
            }

            if (counterSoldQty > 0)
            {
                counterItemDtos.Add(new ExpectedCashItemDto
                {
                    ProductName = product.Name,
                    CategoryName = product.Category.Name,
                    CalculatedSales = counterSoldQty,
                    UnitPrice = product.UnitPrice,
                    SalesValue = product.UnitPrice.HasValue ? revenue : null,
                    IsCounter = true
                });
            }
        }

        var totalSalesRevenue = productSalesTotal + counterSalesTotal;

        // 3. Bugünkü nakit satın alımlar
        var targetDate = DateTime.SpecifyKind(dayClosing.Date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var nextDate = targetDate.AddDays(1);

        var cashPurchases = await _context.Purchases
            .Where(p => p.BranchId == dayClosing.BranchId
                     && p.PurchaseDate >= targetDate && p.PurchaseDate < nextDate
                     && p.PaymentMethod == PaymentMethod.Cash)
            .SumAsync(p => p.TotalAmount);

        // 4 & 5. Bugünkü admin nakit çekim ve yatırımları
        var walletTransactions = await _context.WalletTransactions
            .Where(t => (t.SourceBranchId == dayClosing.BranchId || t.TargetBranchId == dayClosing.BranchId)
                     && t.TransactionDate >= targetDate && t.TransactionDate < nextDate)
            .ToListAsync();

        var cashDeposits = walletTransactions
            .Where(t => t.TransactionType == WalletTransactionType.AdminToBranch
                     && t.WalletType == WalletType.Cash)
            .Sum(t => t.Amount);

        var cashWithdrawals = walletTransactions
            .Where(t => t.TransactionType == WalletTransactionType.BranchToAdmin
                     && t.WalletType == WalletType.Cash)
            .Sum(t => t.Amount);

        // Beklenen nakit = Açılış + (TümGelir - POS) + Yatırım - Satın Alım - Çekim
        var posAmount = dayClosing.PosAmount ?? 0;
        var expectedCash = dayClosing.OpeningCashBalance
            + (totalSalesRevenue - posAmount)
            + cashDeposits
            - cashPurchases
            - cashWithdrawals;

        var response = new ExpectedCashDto
        {
            OpeningCashBalance = dayClosing.OpeningCashBalance,
            CashPurchases = cashPurchases,
            CashWithdrawals = cashWithdrawals,
            CashDeposits = cashDeposits,
            TotalSalesRevenue = totalSalesRevenue,
            CounterSalesTotal = counterSalesTotal,
            ExpectedCashAmount = Math.Max(0, expectedCash),
            ExpectedAmount = totalSalesRevenue, // Backward compatibility
            ProductsWithPrice = productsWithPrice,
            ProductsWithoutPrice = productsWithoutPrice,
            Items = productItems.Concat(counterItemDtos)
                                .OrderBy(i => i.CategoryName)
                                .ThenBy(i => i.ProductName)
                                .ToList()
        };

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

        // The backend `CalculateExpectedCashAsync` already subtracted `closing.PosAmount` from the DB (which is typically 0 at this point).
        // Since we are overriding it with `dto.PosAmount`, we need to adjust the expected cash.
        var baseExpectedAmount = expectedCashRes.Data.ExpectedCashAmount;
        var oldPosAmount = closing.PosAmount ?? 0;
        var newPosAmount = dto.PosAmount;
        
        var adjustedExpectedAmount = baseExpectedAmount + oldPosAmount - newPosAmount;
        var expectedAmount = Math.Max(0, adjustedExpectedAmount); // Tam kasa denklemi sonucu
        
        var totalCounted = dto.CashAmount + dto.PosAmount;
        var diff = dto.CashAmount - expectedAmount; // Nakit farkı

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

        // Counter ürün listesi — her zaman DB'den taze çekilir
        var counterProducts = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.TrackingType == TrackingType.Counter && p.IsActive && !p.IsDeleted)
            .OrderBy(p => p.Category.SortOrder)
            .ThenBy(p => p.SortOrder)
            .Select(p => new CounterProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CategoryName = p.Category.Name,
                UnitPrice = p.UnitPrice,
                Unit = p.Unit.ToString()
            })
            .ToListAsync();

        if (closing == null)
        {
            return ApiResponse<DayClosingSummaryDto>.Ok(new DayClosingSummaryDto
            {
                BranchName = branch.Name,
                Date = date,
                IsClosed = false,
                Items = new List<DailySummaryItemDto>(),
                CounterProducts = counterProducts
            });
        }

        // ADIM 1: Stock tablosunu oku (Anlık stok durumu için)
        var stocks = await _context.Stocks
            .Where(s => s.BranchId == branchId)
            .ToDictionaryAsync(s => s.ProductId);

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
            CounterProducts = counterProducts,
            Items = closing.Details
                .Where(d => d.Product != null && d.Product.IsActive && d.Product.TrackingType != TrackingType.Counter)
                .OrderBy(d => d.Product.Category.SortOrder)
                .ThenBy(d => d.Product.Name)
                .Select(d => {
                    var currentStock = stocks.GetValueOrDefault(d.ProductId);
                    var currentQty = currentStock?.CurrentQuantity ?? 0;
                    
                    return new DailySummaryItemDto
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
                        // Açık günlerde satış henüz hesaplanmaz, sadece stok durumunu gösterir
                        CalculatedSales = closing.IsClosed ? d.CalculatedSales : 0,
                        IsCorrected = d.OriginalEndOfDayCount.HasValue,
                        OriginalEndOfDayCount = d.OriginalEndOfDayCount,
                        OriginalCarryOverQuantity = d.OriginalCarryOverQuantity,
                        LastCorrectionReason = d.CorrectionReason
                    };
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
