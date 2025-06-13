using ReserveHub.Application.UseCases.Reservations.Create;
using ReserveHub.Application.UseCases.Reservations.GetAll;
using ReserveHub.Application.UseCases.Spaces.GetAvailableSpaces;
using ReserveHub.Application.UseCases.Users.GetProfile;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Application.Extensions;

internal static class ReservationExtensions
{
    internal static Reservation ToEntity(this CreateReservationRequest source)
        => new()
        {
            SpaceId = source.SpaceId,
            StartTime = source.StartTime,
            EndTime = source.EndTime,
            Notes = source.Notes ?? string.Empty
        };
    internal static ReservationResponse ToResponse(this Reservation source)
        => new(
            source.Id,
            new UserResponse(source.User.Id, source.User.FirstName, source.User.LastName),
            source.Space.ToResponse(),
            source.StartTime,
            source.EndTime,
            source.RegisterDate,
            source.Status,
            source.Notes ?? string.Empty);
    internal static IReadOnlyList<ReservationResponse> ToResponse(this IReadOnlyList<Reservation> source)
        => [.. source.Select(ToResponse)];
}
