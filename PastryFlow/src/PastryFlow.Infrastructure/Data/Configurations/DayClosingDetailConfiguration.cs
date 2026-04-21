using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class DayClosingDetailConfiguration : IEntityTypeConfiguration<DayClosingDetail>
{
    public void Configure(EntityTypeBuilder<DayClosingDetail> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OpeningStock).HasPrecision(18, 2);
        builder.Property(x => x.ReceivedFromDemands).HasPrecision(18, 2);
        builder.Property(x => x.IncomingTransferQuantity).HasPrecision(18, 2);
        builder.Property(x => x.OutgoingTransferQuantity).HasPrecision(18, 2);
        builder.Property(x => x.DayWasteQuantity).HasPrecision(18, 2);
        builder.Property(x => x.EndOfDayCount).HasPrecision(18, 2);
        builder.Property(x => x.CarryOverQuantity).HasPrecision(18, 2);
        builder.Property(x => x.EndOfDayWaste).HasPrecision(18, 2);
        builder.Property(x => x.CalculatedSales).HasPrecision(18, 2);
        
        builder.Property(x => x.OriginalEndOfDayCount).HasPrecision(18, 2);
        builder.Property(x => x.OriginalCarryOverQuantity).HasPrecision(18, 2);
        builder.Property(x => x.CorrectedEndOfDayCount).HasPrecision(18, 2);
        builder.Property(x => x.CorrectedCarryOverQuantity).HasPrecision(18, 2);
        builder.Property(x => x.CorrectionReason).HasMaxLength(500);

        builder.HasOne(x => x.DayClosing)
            .WithMany(c => c.Details)
            .HasForeignKey(x => x.DayClosingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CorrectedByUser)
            .WithMany()
            .HasForeignKey(x => x.CorrectedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.DayClosingId, x.ProductId }).IsUnique();
    }
}
