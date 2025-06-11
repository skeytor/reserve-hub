using ReserveHub.Application.Handlers;

namespace ReserveHub.Application.UseCases.Users.Update;

public sealed record UpdateUserCommand(UpdateUserRequest User) : ICommand<Guid>;
