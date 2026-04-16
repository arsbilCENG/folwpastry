using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class WasteConfiguration : IEntityTypeConfiguration<Waste>
{
    public void Configure(EntityTypeBuilder<Waste> builder)
    {
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Quantity).HasPrecision(18, 2);
        builder.Property(w => w.Notes).HasMaxLength(500);
        builder.Property(w => w.PhotoPath).HasMaxLength(500);

        builder.HasOne(w => w.Product)
            .WithMany()
            .HasForeignKey(w => w.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Branch)
            .WithMany()
            .HasForeignKey(w => w.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.CreatedByUser)
            .WithMany()
            .HasForeignKey(w => w.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
