using ReserveHub.Domain.Entities;
using SharedKernel;

namespace ReserveHub.Domain.Repositories;

public interface ISpaceRepository
{
    Task<Space> InsertAsync(Space space);
    Task<Space?> GetByIdAsync(int id);
    Task<bool> ExistByNameAsync(string name);
    Task<IReadOnlyList<Space>> GetAvailableSpacesAsync(PaginationParams pagination);
    Task<int> CountAsync();
}
