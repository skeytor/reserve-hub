using ReserveHub.Application.Extensions;
using ReserveHub.Application.Handlers;
using ReserveHub.Application.Messaging;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Errors;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Reservations.Create;

internal sealed class CreateReservationCommandHandler(
    IReservationRepository reservationRepository,
    IUserRepository userRepository,
    IEmailService emailService,
    IConfirmReservationLinkFactory linkGenerator,
    IUnitOfWork unit) 
    : ICommandHandler<CreateReservationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.Reservation.StartTime >= request.Reservation.EndTime)
        {
            return Result.Failure<Guid>(ReservationErrors.DateOutOfRange);
        }
        User? user = await userRepository.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
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
        reservation.UserId = user.Id;
        DateTime utcNow = DateTime.UtcNow;
        NotificationToken notification = new()
        {
            CreatedOnUtc = utcNow,
            ExpiredOnUtc = utcNow.AddDays(1),
            Reservation = reservation
        };
        await reservationRepository.InsertAsync(reservation);
        await reservationRepository.InsertNotificationAsync(notification);
        await unit.SaveChangesAsync(default);

        string link = linkGenerator.Generate(notification);

        await emailService.SendEmail(
            user.Email, 
            "Reservation Pending", 
            $"To confirm your reservation for space " +
            $"{reservation.SpaceId} from {reservation.StartTime} " +
            $"to {reservation.EndTime} <a href='{link}'>click here</a>");
        return reservation.Id;
    }
}
