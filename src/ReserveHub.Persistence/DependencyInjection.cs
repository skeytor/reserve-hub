using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using ReserveHub.Persistence.Abstractions;
using ReserveHub.Persistence.Repositories;
using ReserveHub.Persistence.SampleData;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration) 
        => services
            .AddDatabaseProvider(configuration)
            .AddRepositories()
            .AddUnitOfWork();

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IReservationRepository, ReservationRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ISpaceRepository, SpaceRepository>();

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services) 
        => services
            .AddScoped<IAppDbContext>(options => options.GetRequiredService<ApplicationDbContext>())
            .AddScoped<IUnitOfWork>(options => options.GetRequiredService<ApplicationDbContext>());

    private static IServiceCollection AddDatabaseProvider(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString("Database"),
            optionsAction: options =>
                options.UseSeeding((context, _) =>
                {
                    context.Set<User>().AddRange(SeedData.Users);
                    context.Set<Space>().AddRange(SeedData.Spaces);
                    context.SaveChanges();
                }));
}
