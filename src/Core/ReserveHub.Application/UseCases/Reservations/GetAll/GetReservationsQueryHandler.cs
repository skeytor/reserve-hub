using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Application.Services;
using ReserveHub.Application.UseCases.Spaces.GetAvailableSpaces;
using ReserveHub.Application.UseCases.Users.GetProfile;
using ReserveHub.Domain.Enums;
using ReserveHub.Domain.Repositories;
using SharedKernel;
using SharedKernel.Results;

namespace ReserveHub.Application.UseCases.Reservations.GetAll;

internal class GetReservationsQueryHandler(IReservationRepository repository) : IQueryHandler<GetReservationsQuery, PagedList<ReservationResponse>>
{
    public async Task<Result<PagedList<ReservationResponse>>> Handle(
        GetReservationsQuery request, 
        CancellationToken cancellationToken)
    {
        var reservations = await repository.GetAllAsync(request.Params);
        int totalCount = await repository.CountAsync();
        return PagedList<ReservationResponse>.Create(
            reservations.ToResponse(),
            request.Params.Page,
            request.Params.PageSize,
            totalCount);
    }
}
public sealed record GetReservationsQuery(PaginationParams Params) 
    : IQuery<PagedList<ReservationResponse>>;

public sealed record ReservationResponse(
    Guid Id,
    UserResponse User,
    SpaceResponse Space,
    DateTime StartDate,
    DateTime EndDate,
    DateTime RegisterDate,
    ReservationStatus Status,
    string? Notes);