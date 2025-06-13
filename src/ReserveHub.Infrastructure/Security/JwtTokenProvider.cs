using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReserveHub.Application.Providers;
using ReserveHub.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReserveHub.Infrastructure.Security;

public sealed class JwtTokenProvider(IOptions<TokenOptions> options) : IJwtTokenProvider
{
    private readonly TokenOptions options = options.Value;
    public string GenerateToken(User user)
    {
        string role = user.IsAdministrator ? "Admin" : "User";
        Claim[] claims =
[
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("role", role),
        ];

        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        DateTime expirationTime = DateTime.UtcNow.AddHours(1);

        JwtSecurityToken securityToken = new(
            options.Issuer,
            options.Audience,
            claims,
            null,
            expirationTime,
            signingCredentials);
        string accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return accessToken;
    }
}
