using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Errors;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Users.Update;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unit) : ICommandHandler<UpdateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByEmailAsync(request.User.Email);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }
        if (user.Email != request.User.Email &&
            await userRepository.ExistEmailAsync(request.User.Email))
        {
            return Result.Failure<Guid>(UserErrors.IsAlreadyBy(request.User.Email));
        }
        user.UpdateEntity(request.User);
        await unit.SaveChangesAsync(default);
        return user.Id;
    }
}
