using Microsoft.EntityFrameworkCore;
using ReserveHub.Persistence;

namespace ReserveHub.API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}
