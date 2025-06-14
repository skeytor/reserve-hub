using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveHub.Domain.Entities;
using ReserveHub.Persistence.Helpers;

namespace ReserveHub.Persistence.Configurations;

internal sealed class SpaceConfig : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.ToTable(TableNames.CommunitySpaces);
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.Property(x => x.IsAvailable)
            .IsRequired();
    }
}