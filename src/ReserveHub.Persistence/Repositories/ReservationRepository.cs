using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;

namespace ReserveHub.Persistence.Repositories;

internal sealed class ReservationRepository(ApplicationDbContext context) 
    : BaseRepository(context), IReservationRepository
{
    public Task<IReadOnlyList<Reservation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Reservation?> GetByIdAsync(Guid id) 
        => await Context.Reservations.FindAsync(id);

    public async Task<IReadOnlyList<Reservation>> GetBySpaceIdAsync(Guid spaceId) 
        => await Context
            .Reservations
            .Where(x => x.SpaceId == spaceId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IReadOnlyList<Reservation>> GetByUserIdAsync(Guid userId) 
        => await Context
            .Reservations
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Reservation> InsertAsync(Reservation reservation)
    {
        await Context.Reservations.AddAsync(reservation);
        return reservation;
    }

    public Task<bool> IsSpaceAvailableAsync(Guid spaceId, DateTime startTime, DateTime endTime) 
        => Context
        .Reservations
        .AnyAsync(r => r.SpaceId == spaceId &&
                       r.StartTime < endTime &&
                       r.EndTime > startTime);
}
