using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Reservations.Create;

internal sealed class CreateReservationCommandHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unit) 
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
        Reservation reservation = request.Reservation.ToEntity();
        await reservationRepository.InsertAsync(reservation);
        await unit.SaveChangesAsync(default);
        return reservation.Id;
    }
}
