using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ReserveHub.Domain.Entities;
using System.Reflection;

namespace ReserveHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        return services;
    }
}
