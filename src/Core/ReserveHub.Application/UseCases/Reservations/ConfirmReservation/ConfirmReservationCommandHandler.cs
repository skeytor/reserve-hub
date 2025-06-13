using ReserveHub.Application.Handlers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Enums;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using SharedKernel.UnitOfWork;

namespace ReserveHub.Application.UseCases.Reservations.ConfirmReservation;

internal sealed class ConfirmReservationCommandHandler(
    IReservationRepository repository,
    IUnitOfWork unit) 
    : ICommandHandler<ConfirmReservationCommand>
{
    public async Task<Result> Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
    {
        NotificationToken? token = await repository.GetTokenNotificationAsync(request.TokenId);
        if (token is null)
        {
            return Result.Failure(Error.Failure("", ""));
        }
        token.Reservation.Status = ReservationStatus.Approved;
        repository.DeleteTokenNotification(token);
        await unit.SaveChangesAsync(default);
        return Result.Success();
    }
}
public sealed record ConfirmReservationCommand(Guid TokenId) : ICommand;
