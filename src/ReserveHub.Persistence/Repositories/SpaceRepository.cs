using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
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
        => Context
            .Spaces
            .AsNoTracking()
            .AnyAsync(space => space.Name == name);

    public async Task<IReadOnlyList<Space>> GetAllAsync(PaginationParams queryParams) 
        => await SpecificationEvaluator
        .GetQuery(Context.Spaces.AsQueryable(), new GetAllSpacesSpec(queryParams))
        .AsNoTracking()
        .ToListAsync();

    public async Task<IReadOnlyList<Space>> GetAvailableSpacesAsync(
        DateTime startDate,
        DateTime endDate,
        PaginationParams queryParam) 
        => await SpecificationEvaluator
            .GetQuery(Context.Spaces.AsQueryable(), new GetAvailableSpacesSpec(startDate, endDate, queryParam))
            .AsNoTracking()
            .ToListAsync();

    public async Task<Space?> GetByIdAsync(int id) => await Context.Spaces.FindAsync(id);

    public async Task<Space> InsertAsync(Space space)
    {
        await Context.Spaces.AddAsync(space);
        return space;
    }
}
