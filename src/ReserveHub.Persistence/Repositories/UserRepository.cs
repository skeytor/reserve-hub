using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;

namespace ReserveHub.Persistence.Repositories;

internal sealed class UserRepository(ApplicationDbContext context) 
    : BaseRepository(context), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id) => await Context.Users.FindAsync(id);

    public async Task<User> InsertAsync(User user)
    {
        await Context.Users.AddAsync(user);
        return user;
    }
}
