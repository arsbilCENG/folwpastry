using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data;

public class PastryFlowDbContext : DbContext, IPastryFlowDbContext
{
    public PastryFlowDbContext(DbContextOptions<PastryFlowDbContext> options) : base(options) { }

    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Demand> Demands => Set<Demand>();
    public DbSet<DemandItem> DemandItems => Set<DemandItem>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<TransferItem> TransferItems => Set<TransferItem>();
    public DbSet<DayClosing> DayClosings => Set<DayClosing>();
    public DbSet<DayClosingDetail> DayClosingDetails => Set<DayClosingDetail>();
    public DbSet<Waste> Wastes => Set<Waste>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<DeliveryReturn> DeliveryReturns => Set<DeliveryReturn>();
    public DbSet<CakeOption> CakeOptions => Set<CakeOption>();
    public DbSet<CustomCakeOrder> CustomCakeOrders => Set<CustomCakeOrder>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();
    public DbSet<BranchWallet> BranchWallets { get; set; }
    public DbSet<AdminWallet> AdminWallets { get; set; }
    public DbSet<WalletTransaction> WalletTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Soft Delete Filters
        modelBuilder.Entity<Branch>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);

        // BranchWallet — şube başına 2 kayıt (Cash + Bank), unique index
        modelBuilder.Entity<BranchWallet>()
            .HasIndex(w => new { w.BranchId, w.WalletType })
            .IsUnique();

        modelBuilder.Entity<BranchWallet>()
            .Property(w => w.CurrentBalance)
            .HasPrecision(18, 2);

        // AdminWallet — 2 kayıt (Cash + Bank), unique index
        modelBuilder.Entity<AdminWallet>()
            .HasIndex(w => w.WalletType)
            .IsUnique();

        modelBuilder.Entity<AdminWallet>()
            .Property(w => w.CurrentBalance)
            .HasPrecision(18, 2);

        // WalletTransaction
        modelBuilder.Entity<WalletTransaction>()
            .Property(w => w.Amount)
            .HasPrecision(18, 2);

        // Navigation property'ler için DeleteBehavior.Restrict
        // (cascade delete döngüsü engellemek için)
        modelBuilder.Entity<WalletTransaction>()
            .HasOne(t => t.SourceBranchWallet)
            .WithMany()
            .HasForeignKey(t => t.SourceBranchWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WalletTransaction>()
            .HasOne(t => t.TargetBranchWallet)
            .WithMany()
            .HasForeignKey(t => t.TargetBranchWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WalletTransaction>()
            .HasOne(t => t.SourceAdminWallet)
            .WithMany()
            .HasForeignKey(t => t.SourceAdminWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<WalletTransaction>()
            .HasOne(t => t.TargetAdminWallet)
            .WithMany()
            .HasForeignKey(t => t.TargetAdminWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        // CreatedBy + CreatedByUserId: convention would use shadow FK "CreatedById"; map explicitly.
        modelBuilder.Entity<WalletTransaction>()
            .HasOne(t => t.CreatedBy)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // DayClosing
        modelBuilder.Entity<DayClosing>(entity =>
        {
            entity.Property(e => e.OpeningCashBalance)
                .HasPrecision(18, 2)
                .HasDefaultValue(0m);
            
            entity.Property(e => e.ExpectedCashAmount).HasPrecision(18, 2);
            entity.Property(e => e.CashAmount).HasPrecision(18, 2);
            entity.Property(e => e.PosAmount).HasPrecision(18, 2);
            entity.Property(e => e.TotalCounted).HasPrecision(18, 2);
            entity.Property(e => e.CashDifference).HasPrecision(18, 2);
        });

        // Purchase
        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PurchaseNumber).HasMaxLength(20).IsRequired();
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            entity.HasOne(e => e.Branch).WithMany().HasForeignKey(e => e.BranchId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(e => e.Items).WithOne(i => i.Purchase).HasForeignKey(i => i.PurchaseId).OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(e => !e.IsDeleted);
            entity.HasIndex(e => e.PurchaseNumber).IsUnique();
            entity.HasIndex(e => new { e.BranchId, e.PurchaseDate });
        });

        // PurchaseItem
        modelBuilder.Entity<PurchaseItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            entity.HasOne(e => e.Product).WithMany().HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
            entity.HasQueryFilter(e => !e.IsDeleted);
        });

        // Transfer
        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.SenderBranch)
            .WithMany()
            .HasForeignKey(t => t.SenderBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ReceiverBranch)
            .WithMany()
            .HasForeignKey(t => t.ReceiverBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.CreatedBy)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ReceivedBy)
            .WithMany()
            .HasForeignKey(t => t.ReceivedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.CancelledBy)
            .WithMany()
            .HasForeignKey(t => t.CancelledByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TransferItem>()
            .Property(i => i.Quantity)
            .HasPrecision(18, 2);

        // Soft delete global filter
        modelBuilder.Entity<Transfer>()
            .HasQueryFilter(t => !t.IsDeleted);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedData.Initialize(modelBuilder);
    }
}
