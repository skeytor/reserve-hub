namespace ReserveHub.Infrastructure.Security;

public record class TokenOptions
{
    public required string SecretKey { get; init; } = string.Empty;
    public required string Issuer { get; init; } = string.Empty;
    public required string Audience { get; init; } = string.Empty;
};
