using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class DailyStockSummaryConfiguration : IEntityTypeConfiguration<DailyStockSummary>
{
    public void Configure(EntityTypeBuilder<DailyStockSummary> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.OpeningStock).HasPrecision(18, 2);
        builder.Property(s => s.ReceivedFromDemands).HasPrecision(18, 2);
        builder.Property(s => s.IncomingTransferQuantity).HasPrecision(18, 2);
        builder.Property(s => s.OutgoingTransferQuantity).HasPrecision(18, 2);
        builder.Property(s => s.DayWasteQuantity).HasPrecision(18, 2);
        builder.Property(s => s.EndOfDayCount).HasPrecision(18, 2);
        builder.Property(s => s.CarryOverQuantity).HasPrecision(18, 2);
        builder.Property(s => s.EndOfDayWaste).HasPrecision(18, 2);
        builder.Property(s => s.CalculatedSales).HasPrecision(18, 2);

        builder.HasIndex(s => new { s.BranchId, s.ProductId, s.Date }).IsUnique();

        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
