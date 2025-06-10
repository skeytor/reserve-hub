using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;

namespace ReserveHub.Persistence.Repositories;

internal abstract class SpaceRepository(ApplicationDbContext context) 
    : BaseRepository(context), ISpaceRepository
{
    public async Task<IReadOnlyList<Space>> GetAllAsync() 
        => await Context
            .Spaces
            .AsNoTracking()
            .ToListAsync();

    public async Task<Space?> GetByIdAsync(Guid id) => await Context.Spaces.FindAsync(id);

    public async Task<Space> InsertAsync(Space space)
    {
        await Context.Spaces.AddAsync(space);
        return space;
    }
}
