using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReserveHub.Domain.Entities;
using ReserveHub.Persistence.Helpers;

namespace ReserveHub.Persistence.Configurations;

internal sealed class NotificationTokenConfig : IEntityTypeConfiguration<NotificationToken>
{
    public void Configure(EntityTypeBuilder<NotificationToken> builder)
    {
        builder.ToTable(TableNames.Notifications);

        builder.HasOne(x => x.Reservation)
            .WithMany()
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
