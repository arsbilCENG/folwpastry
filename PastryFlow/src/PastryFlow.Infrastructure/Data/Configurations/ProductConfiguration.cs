using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Property(p => p.UnitPrice).HasPrecision(18, 2);
        
        builder.Property(p => p.TrackingType)
            .HasDefaultValue(TrackingType.Production);

        builder.HasOne(p => p.ProductionBranch)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.ProductionBranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
