using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text;

namespace ReserveHub.Infrastructure.Security
{
    internal sealed class JwtBearerConfigureOptions(IOptions<TokenOptions> options)
        : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly TokenOptions options = options.Value;
        public void Configure(string? name, JwtBearerOptions options)
            => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = this.options.Issuer,
                ValidAudience = this.options.Audience,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(this.options.SecretKey))
            };
        }
    }
}
