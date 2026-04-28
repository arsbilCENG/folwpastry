using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PastryFlow.Domain.Entities;

namespace PastryFlow.Infrastructure.Data.Configurations;

public class CakeOptionConfiguration : IEntityTypeConfiguration<CakeOption>
{
    public void Configure(EntityTypeBuilder<CakeOption> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
        
        builder.HasIndex(e => new { e.OptionType, e.Name }).IsUnique();
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
