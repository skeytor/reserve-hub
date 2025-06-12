using ReserveHub.Domain.Entities;
using SharedKernel;

namespace ReserveHub.Domain.Repositories;

public interface ISpaceRepository
{
    Task<Space> InsertAsync(Space communitySpace);
    Task<Space?> GetByIdAsync(int id);
    Task<bool> ExistByNameAsync(string name);
    Task<IReadOnlyList<Space>> GetAllAsync(PaginationParams pagination);
    Task<IReadOnlyList<Space>> GetAvailableSpacesAsync(
        DateTime starDate, 
        DateTime endDate, 
        PaginationParams pagination);
    Task<int> CountAsync();
}
