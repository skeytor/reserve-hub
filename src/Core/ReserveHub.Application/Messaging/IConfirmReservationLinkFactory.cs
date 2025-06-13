using ReserveHub.Domain.Entities;

namespace ReserveHub.Application.Messaging;

public interface IConfirmReservationLinkFactory
{
    string Generate(NotificationToken notification);
}
