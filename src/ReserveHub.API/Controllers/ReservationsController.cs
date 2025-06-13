using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.Services;
using ReserveHub.Application.UseCases.Reservations.Create;
using ReserveHub.Application.UseCases.Reservations.GetAll;
using SharedKernel;

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

    [HttpGet]
    [ProducesResponseType<PagedList<ReservationResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<PagedList<ReservationResponse>>, BadRequest<ValidationProblemDetails>>> GetAll(
        [FromQuery] PaginationParams paginationParams)
    {
        var command = new GetReservationsQuery(paginationParams);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.ToProblemDetails());
    }

}
