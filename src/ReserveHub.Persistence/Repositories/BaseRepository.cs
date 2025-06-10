using ReserveHub.Persistence.Abstractions;

namespace ReserveHub.Persistence.Repositories;

internal abstract class BaseRepository(IAppDbContext context)
{
    protected readonly IAppDbContext Context = context;
}
