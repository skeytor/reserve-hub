using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;

namespace ReserveHub.Application.UseCases.Users.Create;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository) 
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if ( await userRepository.ExistEmailAsync(request.User.Email))
        {
            return Result.Failure<Guid>(Error.Failure(
                "", 
                $"Email {request.User.Email} already exists. Please use a different email."));
        }
        User user = request.User.ToEntity();
        await userRepository.InsertAsync(user);
        return user.Id;
    }
}
