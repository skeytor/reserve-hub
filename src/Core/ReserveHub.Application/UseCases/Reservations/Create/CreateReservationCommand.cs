using ReserveHub.Application.Handlers;

namespace ReserveHub.Application.UseCases.Reservations.Create;

public sealed record CreateReservationCommand(
    CreateReservationRequest Reservation,
    Guid UserId) : ICommand<Guid>;
