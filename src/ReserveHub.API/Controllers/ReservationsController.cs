using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.Services;
using ReserveHub.Application.UseCases.Reservations.ConfirmReservation;
using ReserveHub.Application.UseCases.Reservations.Create;
using ReserveHub.Application.UseCases.Reservations.GetAll;
using SharedKernel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReserveHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReservationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<Guid>, BadRequest<ValidationProblemDetails>, UnauthorizedHttpResult>> MakeReservation(
        [FromBody] CreateReservationRequest request)
    {
        Guid id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new CreateReservationCommand(request, id);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.ToProblemDetails());
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
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

    [HttpGet("confirm", Name = nameof(ConfirmReservation))]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmReservation([FromQuery] Guid token)
    {
        var command = new ConfirmReservationCommand(token);
        var result = await sender.Send(command);
        return result.IsSuccess ? Ok("Reservation confirmed successfully") : BadRequest("Token Expired");
    }

}
