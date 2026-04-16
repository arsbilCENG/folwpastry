using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TransferNumber).IsRequired().HasMaxLength(50);
        builder.Property(t => t.Notes).HasMaxLength(500);

        builder.HasOne(t => t.FromBranch)
            .WithMany()
            .HasForeignKey(t => t.FromBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToBranch)
            .WithMany()
            .HasForeignKey(t => t.ToBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Items)
            .WithOne(i => i.Transfer)
            .HasForeignKey(i => i.TransferId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
