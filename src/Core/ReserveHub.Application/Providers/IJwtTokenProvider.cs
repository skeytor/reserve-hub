using ReserveHub.Domain.Entities;

namespace ReserveHub.Application.Providers;

public interface IJwtTokenProvider
{
    string GenerateToken(User user);
}
