using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Application.Services;
using ReserveHub.Domain.Repositories;
using SharedKernel;
using SharedKernel.Results;

namespace ReserveHub.Application.UseCases.Spaces.GetAvailableSpaces;

internal class GetAvailableSpacesQueryHandler(ISpaceRepository spaceRepository) 
    : IQueryHandler<GetAvailableSpacesQuery, PagedList<SpaceResponse>>
{
    public async Task<Result<PagedList<SpaceResponse>>> Handle(
        GetAvailableSpacesQuery request, 
        CancellationToken cancellationToken)
    {
        var spaces = await spaceRepository.GetAvailableSpacesAsync(
            request.StartDate,
            request.EndDate,
            request.Pagination);
        
        int totalCount = await spaceRepository.CountAsync();

        return PagedList<SpaceResponse>.Create(
            spaces.ToResponse(),
            request.Pagination.Page,
            request.Pagination.PageSize,
            totalCount);
    }
}
public sealed record GetAvailableSpacesQuery(PaginationParams Pagination, DateTime StartDate, DateTime EndDate) 
    : IQuery<PagedList<SpaceResponse>>;

public sealed record SpaceResponse(int Id, string Name, string Description, bool IsAvailable);