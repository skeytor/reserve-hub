using ReserveHub.Domain.Entities;

namespace ReserveHub.Domain.Repositories;

public interface IUserRepository
{
    Task<User> InsertAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> FindByEmailAsync(string email);
    Task<bool> ExistEmailAsync(string email);
}
