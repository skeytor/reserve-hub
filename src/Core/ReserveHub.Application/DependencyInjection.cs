using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ReserveHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}
