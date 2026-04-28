using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> entity)
    {
        entity.HasKey(e => e.Id);
        
        entity.HasIndex(e => e.UserId);
        entity.HasIndex(e => e.BranchId);
        entity.HasIndex(e => e.IsRead);
        entity.HasIndex(e => e.CreatedAt);
        entity.HasIndex(e => new { e.BranchId, e.IsRead });
        
        entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
        entity.Property(e => e.Message).HasMaxLength(500).IsRequired();
        entity.Property(e => e.SourceEntity).HasMaxLength(100);
        entity.Property(e => e.SourceBranchName).HasMaxLength(200);
        entity.Property(e => e.TargetRole).HasMaxLength(50);
        
        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);
            
        entity.HasOne(e => e.Branch)
            .WithMany()
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
