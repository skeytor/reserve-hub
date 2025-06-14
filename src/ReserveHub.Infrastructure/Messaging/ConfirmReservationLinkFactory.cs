using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ReserveHub.Application.Messaging;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Infrastructure.Messaging;

internal sealed class ConfirmReservationLinkFactory(
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator) : IConfirmReservationLinkFactory
{
    public string Generate(NotificationToken notificationToken)
    {
        string? confirmationLink = linkGenerator
            .GetUriByName(httpContextAccessor.HttpContext!,
            "ConfirmReservation",
            new { token = notificationToken.Id });
        return confirmationLink ?? throw new Exception("Could not  create the confirmed link");
    }
};
