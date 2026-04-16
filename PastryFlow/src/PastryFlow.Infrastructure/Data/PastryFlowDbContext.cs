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
    public DbSet<DailyStockSummary> DailyStockSummaries => Set<DailyStockSummary>();
    public DbSet<Waste> Wastes => Set<Waste>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedData.Initialize(modelBuilder);
    }
}
