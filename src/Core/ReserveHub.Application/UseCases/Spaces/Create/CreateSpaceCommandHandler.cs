using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Errors;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Spaces.Create;

internal sealed class CreateSpaceCommandHandler(
    ISpaceRepository spaceRepository,
    IUnitOfWork unit) : ICommandHandler<CreateSpaceCommand, int>
{
    public async Task<Result<int>> Handle(CreateSpaceCommand request, CancellationToken cancellationToken)
    {
        if (await spaceRepository.ExistByNameAsync(request.Space.Name))
        {
            return Result.Failure<int>(SpaceErrors.SpaceAlreadyExists);
        }
        Space space = new()
        {
            Name = request.Space.Name,
            Description = request.Space.Description ?? string.Empty
        };
        await spaceRepository.InsertAsync(space);
        await unit.SaveChangesAsync(default);
        return space.Id;
    }
}

public sealed record CreateSpaceCommand(CreateSpaceRequest Space) : ICommand<int>;

public sealed record CreateSpaceRequest(
    [Required, MaxLength(50)] string Name,
    [MaxLength(150)] string? Description
);
