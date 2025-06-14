using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using ReserveHub.Persistence.Specifications;
using SharedKernel;

namespace ReserveHub.Persistence.Repositories;

internal sealed class ReservationRepository(ApplicationDbContext context)
    : BaseRepository(context), IReservationRepository
{
    public Task<int> CountAsync()
        => Context.Reservations
            .AsNoTracking()
            .CountAsync();

    public void DeleteTokenNotification(NotificationToken token) 
        => Context.NotificationTokens.Remove(token);

    public async Task<IReadOnlyList<Reservation>> GetAllAsync(PaginationParams pagination)
    {
        return await ApplySpecification(new GetReservationsWitSpacesSpec(pagination))
           .AsNoTracking()
           .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
        => await Context.Reservations.FindAsync(id);

    public Task<NotificationToken?> GetTokenNotificationAsync(Guid tokenId) 
        => Context.NotificationTokens
            .Include(x => x.Reservation)
            .FirstOrDefaultAsync(x => x.Id == tokenId);

    public async Task<Reservation> InsertAsync(Reservation reservation)
    {
        await Context.Reservations.AddAsync(reservation);
        return reservation;
    }

    public async Task<NotificationToken> InsertNotificationAsync(NotificationToken notificationToken)
    {
        await Context.NotificationTokens.AddAsync(notificationToken);
        return notificationToken;
    }

    public async Task<bool> IsSpaceAvailableAsync(int spaceId, DateTime startTime, DateTime endTime)
        => !await Context
        .Reservations
        .AnyAsync(r => r.SpaceId == spaceId &&
                       r.StartTime < endTime &&
                       r.EndTime > startTime);
}
