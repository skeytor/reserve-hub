using ReserveHub.Domain.Entities;

namespace ReserveHub.Domain.Repositories;

public interface ISpaceRepository
{
    Task<Space> InsertAsync(Space communitySpace);
    Task<Space?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Space>> GetAllAsync();
}
