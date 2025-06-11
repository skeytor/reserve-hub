using ReserveHub.Domain.Entities;

namespace ReserveHub.Domain.Repositories;

public interface ISpaceRepository
{
    Task<Space> InsertAsync(Space communitySpace);
    Task<Space?> GetByIdAsync(int id);
    Task<bool> ExistByNameAsync(string name);
    Task<IReadOnlyList<Space>> GetAllAsync();
}
