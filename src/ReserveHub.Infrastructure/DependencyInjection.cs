using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReserveHub.Application.Providers;
using ReserveHub.Infrastructure.Security;

namespace ReserveHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationProvider(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddProviders();
        services
            .Configure<TokenOptions>(configuration.GetSection("Jwt"))
            .ConfigureOptions<JwtBearerConfigureOptions>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        return services;
    }
    private static IServiceCollection AddEmailService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
            .AddSmtpSender(configuration["Email:Host"], configuration.GetValue<int>("Email:Port"));
        return services;
    }
    private static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        return services;
    }
}
