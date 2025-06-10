using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReserveHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseProvider(
        this IServiceCollection services, 
        IConfiguration configuration) =>
        services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString(""));
}
