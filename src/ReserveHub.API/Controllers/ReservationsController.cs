using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.UseCases.Reservations.Create;

namespace ReserveHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<Guid>, BadRequest<ValidationProblemDetails>>> MakeReservation(
        [FromBody] CreateReservationRequest request,
        [FromQuery] Guid userId)
    {
        var command = new CreateReservationCommand(request, userId);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.ToProblemDetails());
    }
}
