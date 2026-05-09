using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Application.Interfaces;

public interface IPastryFlowDbContext
{
    DbSet<Branch> Branches { get; }
    DbSet<User> Users { get; }
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<Demand> Demands { get; }
    DbSet<DemandItem> DemandItems { get; }
    DbSet<Transfer> Transfers { get; }
    DbSet<TransferItem> TransferItems { get; }
    DbSet<DayClosing> DayClosings { get; }
    DbSet<DayClosingDetail> DayClosingDetails { get; }
    DbSet<Waste> Wastes { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<DeliveryReturn> DeliveryReturns { get; }
    DbSet<CakeOption> CakeOptions { get; }
    DbSet<CustomCakeOrder> CustomCakeOrders { get; }
    DbSet<Stock> Stocks { get; }
    DbSet<Purchase> Purchases { get; }
    DbSet<PurchaseItem> PurchaseItems { get; }
    DbSet<BranchWallet> BranchWallets { get; }
    DbSet<AdminWallet> AdminWallets { get; }
    DbSet<WalletTransaction> WalletTransactions { get; }
    Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
    Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
