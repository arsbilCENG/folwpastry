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
    public DbSet<CashTransaction> CashTransactions { get; set; }

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

        // CashTransaction
        modelBuilder.Entity<CashTransaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasQueryFilter(e => !e.IsDeleted);
            entity.HasIndex(e => new { e.BranchId, e.TransactionDate });
        });

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

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedData.Initialize(modelBuilder);
    }
}
