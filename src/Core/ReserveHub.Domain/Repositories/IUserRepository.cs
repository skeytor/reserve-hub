using ReserveHub.Domain.Entities;

namespace ReserveHub.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid> InsertAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
