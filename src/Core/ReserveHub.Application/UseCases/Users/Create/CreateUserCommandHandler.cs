using Microsoft.AspNetCore.Identity;
using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Errors;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Users.Create;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IUnitOfWork unit) 
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if ( await userRepository.ExistEmailAsync(request.User.Email))
        {
            return Result.Failure<Guid>(UserErrors.IsAlreadyBy(request.User.Email));
        }
        User user = request.User.ToEntity();
        user.PasswordHash = passwordHasher.HashPassword(user, request.User.Password);
        await userRepository.InsertAsync(user);
        await unit.SaveChangesAsync(default);
        return user.Id;
    }
}
