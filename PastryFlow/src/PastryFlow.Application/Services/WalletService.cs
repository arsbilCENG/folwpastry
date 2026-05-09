using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.DTOs.Wallet;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Services;

public class WalletService : IWalletService
{
    private readonly IPastryFlowDbContext _context;

    public WalletService(IPastryFlowDbContext context)
    {
        _context = context;
    }

    private async Task<BranchWallet> GetOrCreateBranchWalletAsync(Guid branchId, WalletType type)
    {
        var wallet = await _context.BranchWallets
            .FirstOrDefaultAsync(w => w.BranchId == branchId && w.WalletType == type);

        if (wallet == null)
        {
            wallet = new BranchWallet
            {
                Id = Guid.NewGuid(),
                BranchId = branchId,
                WalletType = type,
                CurrentBalance = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.BranchWallets.Add(wallet);
        }
        return wallet;
    }

    private async Task<AdminWallet> GetOrCreateAdminWalletAsync(WalletType type)
    {
        var wallet = await _context.AdminWallets
            .FirstOrDefaultAsync(w => w.WalletType == type);

        if (wallet == null)
        {
            wallet = new AdminWallet
            {
                Id = Guid.NewGuid(),
                WalletType = type,
                CurrentBalance = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.AdminWallets.Add(wallet);
        }
        return wallet;
    }

    public async Task<WalletSummaryDto> GetWalletSummaryAsync()
    {
        var branches = await _context.Branches
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        var summary = new WalletSummaryDto();

        foreach (var branch in branches)
        {
            var cashWallet = await GetOrCreateBranchWalletAsync(branch.Id, WalletType.Cash);
            var bankWallet = await GetOrCreateBranchWalletAsync(branch.Id, WalletType.Bank);

            summary.Branches.Add(new BranchWalletDto
            {
                BranchId = branch.Id,
                BranchName = branch.Name,
                CashBalance = cashWallet.CurrentBalance,
                BankBalance = bankWallet.CurrentBalance,
                TotalBalance = cashWallet.CurrentBalance + bankWallet.CurrentBalance
            });
        }

        var adminCash = await GetOrCreateAdminWalletAsync(WalletType.Cash);
        var adminBank = await GetOrCreateAdminWalletAsync(WalletType.Bank);

        summary.Admin = new AdminWalletDto
        {
            CashBalance = adminCash.CurrentBalance,
            BankBalance = adminBank.CurrentBalance,
            TotalBalance = adminCash.CurrentBalance + adminBank.CurrentBalance
        };

        summary.GrandTotalCash = summary.Branches.Sum(b => b.CashBalance) + summary.Admin.CashBalance;
        summary.GrandTotalBank = summary.Branches.Sum(b => b.BankBalance) + summary.Admin.BankBalance;
        summary.GrandTotal = summary.GrandTotalCash + summary.GrandTotalBank;

        if (_context.ChangeTracker.HasChanges())
        {
            await _context.SaveChangesAsync();
        }

        return summary;
    }

    public async Task<BranchWalletDto> GetBranchWalletAsync(Guid branchId)
    {
        var branch = await _context.Branches.FindAsync(branchId)
            ?? throw new Exception("Şube bulunamadı.");

        var cashWallet = await GetOrCreateBranchWalletAsync(branchId, WalletType.Cash);
        var bankWallet = await GetOrCreateBranchWalletAsync(branchId, WalletType.Bank);

        if (_context.ChangeTracker.HasChanges())
        {
            await _context.SaveChangesAsync();
        }

        return new BranchWalletDto
        {
            BranchId = branchId,
            BranchName = branch.Name,
            CashBalance = cashWallet.CurrentBalance,
            BankBalance = bankWallet.CurrentBalance,
            TotalBalance = cashWallet.CurrentBalance + bankWallet.CurrentBalance
        };
    }

    public async Task SetInitialBalanceAsync(SetInitialBalanceRequest request, Guid adminUserId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if (request.BranchId.HasValue)
            {
                var branch = await _context.Branches.FindAsync(request.BranchId.Value)
                    ?? throw new Exception("Şube bulunamadı.");

                await UpdateInitialBalance(WalletPartyType.Branch, request.BranchId.Value, WalletType.Cash, request.CashBalance, adminUserId);
                await UpdateInitialBalance(WalletPartyType.Branch, request.BranchId.Value, WalletType.Bank, request.BankBalance, adminUserId);
            }
            else
            {
                await UpdateInitialBalance(WalletPartyType.Admin, null, WalletType.Cash, request.CashBalance, adminUserId);
                await UpdateInitialBalance(WalletPartyType.Admin, null, WalletType.Bank, request.BankBalance, adminUserId);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateInitialBalance(WalletPartyType partyType, Guid? branchId, WalletType walletType, decimal newBalance, Guid userId)
    {
        BaseEntity? walletBase;
        Guid? branchWalletId = null;
        Guid? adminWalletId = null;

        if (partyType == WalletPartyType.Branch)
        {
            var w = await GetOrCreateBranchWalletAsync(branchId!.Value, walletType);
            walletBase = w;
            branchWalletId = w.Id;
        }
        else
        {
            var w = await GetOrCreateAdminWalletAsync(walletType);
            walletBase = w;
            adminWalletId = w.Id;
        }

        decimal oldBalance = 0;
        if (walletBase is BranchWallet bw) oldBalance = bw.CurrentBalance;
        if (walletBase is AdminWallet aw) oldBalance = aw.CurrentBalance;

        var difference = newBalance - oldBalance;
        if (difference == 0) return;

        WalletTransaction? existingTx;
        if (partyType == WalletPartyType.Branch)
        {
            existingTx = await _context.WalletTransactions
                .FirstOrDefaultAsync(t => t.TransactionType == WalletTransactionType.InitialBalance 
                                    && t.TargetType == WalletPartyType.Branch
                                    && t.TargetBranchWalletId == branchWalletId);
        }
        else
        {
            existingTx = await _context.WalletTransactions
                .FirstOrDefaultAsync(t => t.TransactionType == WalletTransactionType.InitialBalance 
                                    && t.TargetType == WalletPartyType.Admin
                                    && t.TargetAdminWalletId == adminWalletId);
        }

        if (existingTx != null)
        {
            existingTx.Amount += difference;
            existingTx.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            existingTx = new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.InitialBalance,
                WalletType = walletType,
                TargetType = partyType,
                TargetBranchId = branchId,
                TargetBranchWalletId = branchWalletId,
                TargetAdminWalletId = adminWalletId,
                Amount = newBalance,
                Description = "Başlangıç bakiyesi ayarlandı",
                CreatedByUserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.WalletTransactions.Add(existingTx);
        }

        if (walletBase is BranchWallet branchW) branchW.CurrentBalance = newBalance;
        if (walletBase is AdminWallet adminW) adminW.CurrentBalance = newBalance;
    }

    public async Task ApplyDayClosingRevenueAsync(Guid branchId, decimal cashRevenue, decimal bankRevenue, Guid closedByUserId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if (cashRevenue > 0)
            {
                var cashWallet = await GetOrCreateBranchWalletAsync(branchId, WalletType.Cash);
                cashWallet.CurrentBalance += cashRevenue;
                cashWallet.UpdatedAt = DateTime.UtcNow;

                _context.WalletTransactions.Add(new WalletTransaction
                {
                    Id = Guid.NewGuid(),
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = WalletTransactionType.DayClosingCash,
                    WalletType = WalletType.Cash,
                    TargetType = WalletPartyType.Branch,
                    TargetBranchId = branchId,
                    TargetBranchWalletId = cashWallet.Id,
                    Amount = cashRevenue,
                    Description = "Gün sonu nakit ciro",
                    CreatedByUserId = closedByUserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            if (bankRevenue > 0)
            {
                var bankWallet = await GetOrCreateBranchWalletAsync(branchId, WalletType.Bank);
                bankWallet.CurrentBalance += bankRevenue;
                bankWallet.UpdatedAt = DateTime.UtcNow;

                _context.WalletTransactions.Add(new WalletTransaction
                {
                    Id = Guid.NewGuid(),
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = WalletTransactionType.DayClosingBank,
                    WalletType = WalletType.Bank,
                    TargetType = WalletPartyType.Branch,
                    TargetBranchId = branchId,
                    TargetBranchWalletId = bankWallet.Id,
                    Amount = bankRevenue,
                    Description = "Gün sonu kart ciro",
                    CreatedByUserId = closedByUserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ApplyPurchaseDeductionAsync(Guid branchId, WalletType walletType, decimal amount, string purchaseNumber, Guid createdByUserId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var wallet = await GetOrCreateBranchWalletAsync(branchId, walletType);

            if (wallet.CurrentBalance < amount)
                throw new Exception($"Yetersiz {walletType} bakiyesi. Mevcut: ₺{wallet.CurrentBalance:N2}");

            wallet.CurrentBalance -= amount;
            wallet.UpdatedAt = DateTime.UtcNow;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.PurchaseDeduction,
                WalletType = walletType,
                SourceType = WalletPartyType.Branch,
                SourceBranchId = branchId,
                SourceBranchWalletId = wallet.Id,
                Amount = amount,
                Description = $"Satın alım kesintisi: {purchaseNumber}",
                CreatedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RevertPurchaseDeductionAsync(Guid branchId, WalletType walletType, decimal amount, string purchaseNumber, Guid createdByUserId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var wallet = await GetOrCreateBranchWalletAsync(branchId, walletType);

            wallet.CurrentBalance += amount;
            wallet.UpdatedAt = DateTime.UtcNow;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.PurchaseRefund,
                WalletType = walletType,
                TargetType = WalletPartyType.Branch,
                TargetBranchId = branchId,
                TargetBranchWalletId = wallet.Id,
                Amount = amount,
                Description = $"Satın alım iptali iadesi: {purchaseNumber}",
                CreatedByUserId = createdByUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task TransferBranchToAdminAsync(TransferRequest request, Guid adminUserId)
    {
        if (request.Amount <= 0) throw new Exception("Transfer miktarı 0'dan büyük olmalıdır.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var branchWallet = await GetOrCreateBranchWalletAsync(request.BranchId, request.WalletType);
            var adminWallet = await GetOrCreateAdminWalletAsync(request.WalletType);

            if (branchWallet.CurrentBalance < request.Amount)
                throw new Exception($"Yetersiz {request.WalletType} bakiyesi. Mevcut: ₺{branchWallet.CurrentBalance:N2}");

            branchWallet.CurrentBalance -= request.Amount;
            branchWallet.UpdatedAt = DateTime.UtcNow;

            adminWallet.CurrentBalance += request.Amount;
            adminWallet.UpdatedAt = DateTime.UtcNow;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.BranchToAdmin,
                WalletType = request.WalletType,
                SourceType = WalletPartyType.Branch,
                SourceBranchId = request.BranchId,
                SourceBranchWalletId = branchWallet.Id,
                TargetType = WalletPartyType.Admin,
                TargetAdminWalletId = adminWallet.Id,
                Amount = request.Amount,
                Description = request.Description ?? "Şubeden admin kasasına transfer",
                CreatedByUserId = adminUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task TransferAdminToBranchAsync(TransferRequest request, Guid adminUserId)
    {
        if (request.Amount <= 0) throw new Exception("Transfer miktarı 0'dan büyük olmalıdır.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var adminWallet = await GetOrCreateAdminWalletAsync(request.WalletType);
            var branchWallet = await GetOrCreateBranchWalletAsync(request.BranchId, request.WalletType);

            if (adminWallet.CurrentBalance < request.Amount)
                throw new Exception($"Yetersiz {request.WalletType} bakiyesi. Mevcut: ₺{adminWallet.CurrentBalance:N2}");

            adminWallet.CurrentBalance -= request.Amount;
            adminWallet.UpdatedAt = DateTime.UtcNow;

            branchWallet.CurrentBalance += request.Amount;
            branchWallet.UpdatedAt = DateTime.UtcNow;

            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.AdminToBranch,
                WalletType = request.WalletType,
                SourceType = WalletPartyType.Admin,
                SourceAdminWalletId = adminWallet.Id,
                TargetType = WalletPartyType.Branch,
                TargetBranchId = request.BranchId,
                TargetBranchWalletId = branchWallet.Id,
                Amount = request.Amount,
                Description = request.Description ?? "Admin kasasından şubeye transfer",
                CreatedByUserId = adminUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ManualAdjustmentAsync(ManualAdjustmentRequest request, Guid adminUserId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            BaseEntity? walletBase;
            Guid? branchWalletId = null;
            Guid? adminWalletId = null;
            WalletPartyType targetType;

            if (request.BranchId.HasValue)
            {
                var w = await GetOrCreateBranchWalletAsync(request.BranchId.Value, request.WalletType);
                if (w.CurrentBalance + request.Amount < 0)
                    throw new Exception($"Yetersiz {request.WalletType} bakiyesi. Mevcut: ₺{w.CurrentBalance:N2}");
                
                w.CurrentBalance += request.Amount;
                w.UpdatedAt = DateTime.UtcNow;
                walletBase = w;
                branchWalletId = w.Id;
                targetType = WalletPartyType.Branch;
            }
            else
            {
                var w = await GetOrCreateAdminWalletAsync(request.WalletType);
                if (w.CurrentBalance + request.Amount < 0)
                    throw new Exception($"Yetersiz {request.WalletType} bakiyesi. Mevcut: ₺{w.CurrentBalance:N2}");
                
                w.CurrentBalance += request.Amount;
                w.UpdatedAt = DateTime.UtcNow;
                walletBase = w;
                adminWalletId = w.Id;
                targetType = WalletPartyType.Admin;
            }

            _context.WalletTransactions.Add(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.UtcNow,
                TransactionType = WalletTransactionType.ManualAdjustment,
                WalletType = request.WalletType,
                TargetType = targetType,
                TargetBranchId = request.BranchId,
                TargetBranchWalletId = branchWalletId,
                TargetAdminWalletId = adminWalletId,
                Amount = request.Amount,
                Description = request.Description,
                CreatedByUserId = adminUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<WalletTransactionDto>> GetTransactionsAsync(Guid? branchId, DateTime? startDate, DateTime? endDate)
    {
        var query = _context.WalletTransactions
            .Include(t => t.CreatedBy)
            .Include(t => t.SourceBranch)
            .Include(t => t.TargetBranch)
            .AsQueryable();

        if (branchId.HasValue)
        {
            query = query.Where(t => t.SourceBranchId == branchId || t.TargetBranchId == branchId);
        }

        if (startDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.TransactionDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));
        }

        var transactions = await query.OrderByDescending(t => t.TransactionDate).ToListAsync();

        return transactions.Select(t => new WalletTransactionDto
        {
            Id = t.Id,
            TransactionDate = t.TransactionDate,
            TransactionTypeLabel = t.TransactionType.ToString(),
            WalletTypeLabel = t.WalletType.ToString(),
            SourceLabel = GetPartyLabel(t.SourceType, t.SourceBranch),
            TargetLabel = GetPartyLabel(t.TargetType, t.TargetBranch),
            Amount = t.Amount,
            Description = t.Description,
            CreatedByName = t.CreatedBy?.Email ?? "Sistem"
        }).ToList();
    }

    private string? GetPartyLabel(WalletPartyType type, Branch? branch)
    {
        if (type == WalletPartyType.Admin) return "Admin";
        if (type == WalletPartyType.Branch && branch != null) return branch.Name;
        return null;
    }
}
