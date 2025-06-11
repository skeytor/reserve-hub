using ReserveHub.Application.Handlers;

namespace ReserveHub.Application.UseCases.Users.Create;

public sealed record CreateUserCommand(CreateUserRequest User) : ICommand<Guid>;
