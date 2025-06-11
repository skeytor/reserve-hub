using Microsoft.EntityFrameworkCore;
using ReserveHub.Domain.Entities;
using ReserveHub.Persistence.Abstractions;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options), IAppDbContext, IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; } = default!;
    public DbSet<Reservation> Reservations { get; } = default!;
    public DbSet<Space> Spaces { get; } = default!;
}