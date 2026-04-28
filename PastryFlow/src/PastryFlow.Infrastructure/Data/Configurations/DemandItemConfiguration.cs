using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class DemandItemConfiguration : IEntityTypeConfiguration<DemandItem>
{
    public void Configure(EntityTypeBuilder<DemandItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.RequestedQuantity).HasPrecision(18, 2);
        builder.Property(i => i.ApprovedQuantity).HasPrecision(18, 2);
        builder.Property(i => i.SentQuantity).HasPrecision(18, 2);
        builder.Property(i => i.AcceptedQuantity).HasPrecision(18, 2);
        builder.Property(i => i.RejectedQuantity).HasPrecision(18, 2);
        builder.Property(i => i.RejectionReason).HasMaxLength(300);
        builder.Property(i => i.DeliveryRejectionReason).HasMaxLength(500);
        builder.Property(i => i.RejectionPhotoUrl).HasMaxLength(500);

        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
