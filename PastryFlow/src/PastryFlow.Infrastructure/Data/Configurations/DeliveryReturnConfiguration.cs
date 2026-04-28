using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class DeliveryReturnConfiguration : IEntityTypeConfiguration<DeliveryReturn>
{
    public void Configure(EntityTypeBuilder<DeliveryReturn> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.DemandId);
        builder.HasIndex(e => e.DemandItemId);
        builder.HasIndex(e => e.FromBranchId);
        builder.HasIndex(e => e.ToBranchId);
        builder.HasIndex(e => e.CreatedAt);
        
        builder.Property(e => e.Quantity).HasPrecision(18, 2);
        builder.Property(e => e.Reason).HasMaxLength(500).IsRequired();
        builder.Property(e => e.PhotoUrl).HasMaxLength(500);
        
        builder.HasOne(e => e.Demand)
            .WithMany()
            .HasForeignKey(e => e.DemandId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(e => e.DemandItem)
            .WithMany()
            .HasForeignKey(e => e.DemandItemId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(e => e.Product)
            .WithMany()
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.FromBranch)
            .WithMany()
            .HasForeignKey(e => e.FromBranchId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(e => e.ToBranch)
            .WithMany()
            .HasForeignKey(e => e.ToBranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
