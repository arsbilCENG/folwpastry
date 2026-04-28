using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class CustomCakeOrderConfiguration : IEntityTypeConfiguration<CustomCakeOrder>
{
    public void Configure(EntityTypeBuilder<CustomCakeOrder> entity)
    {
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
        entity.HasIndex(e => e.OrderNumber).IsUnique();
        
        entity.Property(e => e.CustomerName).HasMaxLength(200);
        entity.Property(e => e.CustomerPhone).HasMaxLength(20);
        entity.Property(e => e.Description).HasMaxLength(2000).IsRequired();
        entity.Property(e => e.ReferencePhotoUrl).HasMaxLength(500);
        entity.Property(e => e.StatusNote).HasMaxLength(500);
        entity.Property(e => e.Price).HasPrecision(18, 2);
        
        entity.HasOne(e => e.Branch)
            .WithMany()
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.ProductionBranch)
            .WithMany()
            .HasForeignKey(e => e.ProductionBranchId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.CakeType)
            .WithMany()
            .HasForeignKey(e => e.CakeTypeId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.InnerCream)
            .WithMany()
            .HasForeignKey(e => e.InnerCreamId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.OuterCream)
            .WithMany()
            .HasForeignKey(e => e.OuterCreamId)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasOne(e => e.StatusChangedByUser)
            .WithMany()
            .HasForeignKey(e => e.StatusChangedByUserId)
            .OnDelete(DeleteBehavior.SetNull);
        
        entity.HasIndex(e => e.BranchId);
        entity.HasIndex(e => e.ProductionBranchId);
        entity.HasIndex(e => e.DeliveryDate);
        entity.HasIndex(e => e.Status);
    }
}
