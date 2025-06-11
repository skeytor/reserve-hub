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
            return Result.Failure<Guid>(
                Error.Failure("", "Start time must be before end time."));
        }

        if ( !await reservationRepository.IsSpaceAvailableAsync(
            request.Reservation.SpaceId, 
            request.Reservation.StartTime, 
            request.Reservation.EndTime))
        {
            return Result.Failure<Guid>(
                Error.Failure("", "The space is not available for the selected time."));
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
