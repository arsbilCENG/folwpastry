using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class DemandConfiguration : IEntityTypeConfiguration<Demand>
{
    public void Configure(EntityTypeBuilder<Demand> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.DemandNumber).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Notes).HasMaxLength(500);

        builder.HasOne(d => d.SalesBranch)
            .WithMany()
            .HasForeignKey(d => d.SalesBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.ProductionBranch)
            .WithMany()
            .HasForeignKey(d => d.ProductionBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.CreatedByUser)
            .WithMany()
            .HasForeignKey(d => d.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Items)
            .WithOne(i => i.Demand)
            .HasForeignKey(i => i.DemandId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
