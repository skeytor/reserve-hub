using ReserveHub.Domain.Specifications;
using ReserveHub.Persistence.Specifications;

namespace ReserveHub.Persistence.Repositories;

internal abstract class BaseRepository(ApplicationDbContext context)
{
    protected readonly ApplicationDbContext Context = context;
    protected IQueryable<TEntity> ApplySpecification<TEntity>(Specification<TEntity> specification)
    where TEntity : class
    => SpecificationEvaluator.GetQuery(Context.Set<TEntity>(), specification);
}
