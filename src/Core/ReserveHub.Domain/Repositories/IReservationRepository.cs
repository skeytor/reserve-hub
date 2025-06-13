using ReserveHub.Domain.Entities;
using SharedKernel;

namespace ReserveHub.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation> InsertAsync(Reservation reservation);
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Reservation>> GetAllAsync(PaginationParams pagination);
    Task<bool> IsSpaceAvailableAsync(int spaceId, DateTime startTime, DateTime endTime);
    Task<int> CountAsync();
}
