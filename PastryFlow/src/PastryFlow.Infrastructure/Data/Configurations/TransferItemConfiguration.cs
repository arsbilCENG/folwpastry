using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class TransferItemConfiguration : IEntityTypeConfiguration<TransferItem>
{
    public void Configure(EntityTypeBuilder<TransferItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Quantity).HasPrecision(18, 2);
        builder.Property(i => i.ReceivedQuantity).HasPrecision(18, 2);
        builder.Property(i => i.Notes).HasMaxLength(300);

        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
