using ReserveHub.Application.UseCases.Users.Create;
using ReserveHub.Application.UseCases.Users.Update;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Application.Extensions;

internal static class UserExtensions
{
    internal static User ToEntity(this CreateUserRequest source) =>
        new()
        {
            Email = source.Email,
            FirstName = source.FirstName,
            LastName = source.LastName,
            IsActive = true,
            IsAdministrator = false
        };
    internal static void UpdateEntity(this User target, UpdateUserRequest source)
    {
        target.FirstName = source.FirstName;
        target.LastName = source.LastName;
        target.Email = source.Email;
    }

}
