using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;

namespace ReserveHub.Persistence.Repositories;

internal sealed class UserRepository(ApplicationDbContext context) 
    : BaseRepository(context), IUserRepository
{
    public Task<bool> ExistEmailAsync(string email) 
        => Context
        .Users
        .AsNoTracking()
        .AnyAsync(u => u.Email == email);

    public Task<User?> FindByEmailAsync(string email) => Context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> FindByIdAsync(Guid id) => await Context.Users.FindAsync(id);

    public async Task<User> InsertAsync(User user)
    {
        await Context.Users.AddAsync(user);
        return user;
    }
}
