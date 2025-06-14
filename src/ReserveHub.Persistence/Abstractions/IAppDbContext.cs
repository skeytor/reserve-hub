using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Persistence.Abstractions;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Reservation> Reservations { get; }
    DbSet<Space> Spaces { get; }
    DbSet<NotificationToken> NotificationTokens { get; }
}
