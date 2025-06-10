using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Persistence.Abstractions;

namespace ReserveHub.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options), IAppDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Reservation> Reservations { get; set; } = default!;
    public DbSet<Space> CommunitySpaces { get; set; } = default!;
}