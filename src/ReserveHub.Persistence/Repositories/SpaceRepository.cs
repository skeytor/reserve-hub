using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Enums;
using ReserveHub.Domain.Repositories;
using ReserveHub.Persistence.Specifications;
using SharedKernel;

namespace ReserveHub.Persistence.Repositories;

internal class SpaceRepository(ApplicationDbContext context) 
    : BaseRepository(context), ISpaceRepository
{
    public Task<int> CountAsync() 
        => Context.Spaces
            .AsNoTracking()
            .CountAsync();

    public Task<bool> ExistByNameAsync(string name) 
        => Context.Spaces
            .AsNoTracking()
            .AnyAsync(space => space.Name == name);

    public async Task<IReadOnlyList<Space>> GetAvailableSpacesAsync(PaginationParams pagination)
        => await ApplySpecification(new GetAvailableSpacesSpec(pagination))
                .Where(s => !s.Reservations.Any())
                .AsNoTracking()
                .ToListAsync();

    public async Task<Space?> GetByIdAsync(int id) => await Context.Spaces.FindAsync(id);

    public async Task<Space> InsertAsync(Space space)
    {
        await Context.Spaces.AddAsync(space);
        return space;
    }
}
