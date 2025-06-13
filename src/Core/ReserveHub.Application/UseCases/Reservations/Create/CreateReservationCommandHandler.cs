using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Application.Messaging;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Reservations.Create;

internal sealed class CreateReservationCommandHandler(
    IReservationRepository reservationRepository,
    IEmailService emailService,
    IUnitOfWork unit) 
    : ICommandHandler<CreateReservationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.Reservation.StartTime >= request.Reservation.EndTime)
        {
            return Result.Failure<Guid>(ReservationErrors.DateOutOfRange);
        }
        int totalHours = (int)(request.Reservation.EndTime - request.Reservation.StartTime).TotalHours;
        if (totalHours < 1 || totalHours > 72)
        {
            return Result.Failure<Guid>(ReservationErrors.InvalidDuration);
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
        reservation.UserId = request.UserId;
        await reservationRepository.InsertAsync(reservation);
        await unit.SaveChangesAsync(default);
        await emailService.SendEmail(
            reservation.User.Email, 
            "Reservation Pending", 
            $"To verify your reservation for space {reservation.Space.Name} from {reservation.StartTime} to {reservation.EndTime} click here");
        return reservation.Id;
    }
}
