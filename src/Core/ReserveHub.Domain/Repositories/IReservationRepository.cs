using ReserveHub.Domain.Entities;

namespace ReserveHub.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation> InsertAsync(Reservation reservation);
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Reservation>> GetAllAsync();
    Task<bool> IsSpaceAvailableAsync(int spaceId, DateTime startTime, DateTime endTime);
    Task<IReadOnlyList<Reservation>> GetByUserIdAsync(Guid userId);
    Task<IReadOnlyList<Reservation>> GetBySpaceIdAsync(int spaceId);
}
