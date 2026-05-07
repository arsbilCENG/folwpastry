using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CashTransactions;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class CashTransactionService : ICashTransactionService
{
    private readonly IPastryFlowDbContext _context;

    public CashTransactionService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    public async Task<CashTransactionDto> CreateTransactionAsync(
        Guid adminUserId, CreateCashTransactionDto dto)
    {
        if (dto.Amount <= 0)
            throw new Exception("Tutar sıfırdan büyük olmalıdır.");

        var branch = await _context.Branches.FindAsync(dto.BranchId)
            ?? throw new Exception("Şube bulunamadı.");

        var transaction = new CashTransaction
        {
            Id = Guid.NewGuid(),
            BranchId = dto.BranchId,
            TransactionDate = dto.TransactionDate.Date,
            TransactionType = dto.TransactionType,
            Amount = dto.Amount,
            Method = dto.Method,
            Description = dto.Description,
            CreatedByUserId = adminUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.CashTransactions.Add(transaction);
        await _context.SaveChangesAsync();

        return await MapToDtoAsync(transaction);
    }

    public async Task<PagedResult<CashTransactionDto>> GetTransactionsAsync(
        Guid branchId, PaginationParams pagination,
        DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.CashTransactions
            .Include(t => t.Branch)
            .Include(t => t.CreatedByUser)
            .Where(t => t.BranchId == branchId)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(t => t.TransactionDate >= startDate.Value.Date);
        if (endDate.HasValue)
            query = query.Where(t => t.TransactionDate <= endDate.Value.Date);

        query = query.OrderByDescending(t => t.TransactionDate)
                     .ThenByDescending(t => t.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<CashTransactionDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = total,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }

    public async Task<PagedResult<CashTransactionDto>> GetAllTransactionsAsync(
        PaginationParams pagination, Guid? branchId = null,
        DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.CashTransactions
            .Include(t => t.Branch)
            .Include(t => t.CreatedByUser)
            .AsQueryable();

        if (branchId.HasValue)
            query = query.Where(t => t.BranchId == branchId.Value);
        if (startDate.HasValue)
            query = query.Where(t => t.TransactionDate >= startDate.Value.Date);
        if (endDate.HasValue)
            query = query.Where(t => t.TransactionDate <= endDate.Value.Date);

        query = query.OrderByDescending(t => t.TransactionDate)
                     .ThenByDescending(t => t.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<CashTransactionDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = total,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }

    public async Task<BranchCashSummaryDto> GetBranchCashSummaryAsync(
        Guid branchId, DateTime date)
    {
        var branch = await _context.Branches.FindAsync(branchId)
            ?? throw new Exception("Şube bulunamadı.");

        var dateOnly = DateOnly.FromDateTime(date);
        var targetDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var nextDate = targetDate.AddDays(1);

        // Bugünkü açık gün (varsa)
        var todayClosing = await _context.DayClosings
            .FirstOrDefaultAsync(dc => dc.BranchId == branchId
                                    && dc.Date == dateOnly);

        // Açılış bakiyesi: bugünkü DayClosing'deki OpeningCashBalance
        var openingBalance = todayClosing?.OpeningCashBalance ?? 0;

        // Bugünkü nakit satış geliri:
        decimal todayCashSales = 0;
        if (todayClosing != null)
        {
            var details = await _context.DayClosingDetails
                .Where(d => d.DayClosingId == todayClosing.Id && !d.IsDeleted)
                .Include(d => d.Product)
                .ToListAsync();

            var totalSalesRevenue = details
                .Where(d => d.Product != null)
                .Sum(d => d.CalculatedSales * (d.Product!.UnitPrice ?? 0));

            var posAmount = todayClosing.PosAmount ?? 0;
            todayCashSales = Math.Max(0, totalSalesRevenue - posAmount);
        }

        // Bugünkü nakit satın alımlar
        var todayCashPurchases = await _context.Purchases
            .Where(p => p.BranchId == branchId
                     && p.PurchaseDate >= targetDate && p.PurchaseDate < nextDate
                     && p.PaymentMethod == PaymentMethod.Cash)
            .SumAsync(p => p.TotalAmount);

        // Bugünkü admin çekimleri (nakit)
        var todayWithdrawals = await _context.CashTransactions
            .Where(t => t.BranchId == branchId
                     && t.TransactionDate >= targetDate && t.TransactionDate < nextDate
                     && t.TransactionType == TransactionType.AdminWithdrawal
                     && t.Method == PaymentMethod.Cash)
            .SumAsync(t => t.Amount);

        // Bugünkü admin yatırımları (nakit)
        var todayDeposits = await _context.CashTransactions
            .Where(t => t.BranchId == branchId
                     && t.TransactionDate >= targetDate && t.TransactionDate < nextDate
                     && t.TransactionType == TransactionType.AdminDeposit
                     && t.Method == PaymentMethod.Cash)
            .SumAsync(t => t.Amount);

        var expectedCashBalance = openingBalance
            + todayCashSales
            + todayDeposits
            - todayCashPurchases
            - todayWithdrawals;

        return new BranchCashSummaryDto
        {
            BranchId = branchId,
            BranchName = branch.Name,
            OpeningCashBalance = openingBalance,
            TodayCashSales = todayCashSales,
            TodayCashPurchases = todayCashPurchases,
            TodayWithdrawals = todayWithdrawals,
            TodayDeposits = todayDeposits,
            ExpectedCashBalance = expectedCashBalance,
            LastCountedCash = todayClosing?.CashAmount,
            LastUpdated = todayClosing?.UpdatedAt
        };
    }

    public async Task<List<BranchCashSummaryDto>> GetAllBranchCashSummariesAsync(DateTime date)
    {
        var branches = await _context.Branches
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        var summaries = new List<BranchCashSummaryDto>();
        foreach (var branch in branches)
        {
            var summary = await GetBranchCashSummaryAsync(branch.Id, date);
            summaries.Add(summary);
        }

        return summaries;
    }

    public async Task DeleteTransactionAsync(Guid id, Guid adminUserId)
    {
        var transaction = await _context.CashTransactions.FindAsync(id)
            ?? throw new Exception("Kayıt bulunamadı.");

        transaction.IsDeleted = true;
        transaction.DeletedAt = DateTime.UtcNow;
        transaction.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    private async Task<CashTransactionDto> MapToDtoAsync(CashTransaction t)
    {
        if (t.Branch == null)
        {
            t.Branch = await _context.Branches.FindAsync(t.BranchId);
        }
        if (t.CreatedByUser == null)
        {
            t.CreatedByUser = await _context.Users.FindAsync(t.CreatedByUserId);
        }
        
        return MapToDto(t);
    }

    private static CashTransactionDto MapToDto(CashTransaction t) => new()
    {
        Id = t.Id,
        BranchId = t.BranchId,
        BranchName = t.Branch?.Name ?? string.Empty,
        TransactionDate = t.TransactionDate,
        TransactionType = t.TransactionType,
        Amount = t.Amount,
        Method = t.Method,
        Description = t.Description,
        CreatedByUserName = t.CreatedByUser?.Email ?? string.Empty,
        CreatedAt = t.CreatedAt
    };
}
