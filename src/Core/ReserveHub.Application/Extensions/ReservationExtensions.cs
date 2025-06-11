using ReserveHub.Application.UseCases.Reservations.Create;
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
}
