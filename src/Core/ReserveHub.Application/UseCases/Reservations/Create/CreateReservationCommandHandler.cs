using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;

namespace ReserveHub.Application.UseCases.Reservations.Create;

internal sealed class CreateReservationCommandHandler(IReservationRepository reservationRepository) 
    : ICommandHandler<CreateReservationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.Reservation.StartTime >= request.Reservation.EndTime)
        {
            return Result.Failure<Guid>(ReservationErrors.DateOutOfRange);
        }
        bool isSpaceAvailable = await reservationRepository.IsSpaceAvailableAsync(
            request.Reservation.SpaceId, 
            request.Reservation.StartTime, 
            request.Reservation.EndTime);
        if (!isSpaceAvailable)
        {
            return Result.Failure<Guid>(ReservationErrors.SpaceNotAvailable);
        }
        Reservation reservation = new()
        {
            UserId = request.Reservation.UserId,
            SpaceId = request.Reservation.SpaceId,
            StartTime = request.Reservation.StartTime,
            EndTime = request.Reservation.EndTime,
            Notes = request.Reservation.Notes ?? string.Empty,
        };
        await reservationRepository.InsertAsync(reservation);
        return reservation.Id;
    }
}
