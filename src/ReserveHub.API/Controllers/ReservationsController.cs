using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.Services;
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
        if (!Guid.TryParse(User.FindFirstValue(JwtRegisteredClaimNames.Sub), out Guid userId) is false)
        {
            return TypedResults.Unauthorized();
        }   
        var command = new CreateReservationCommand(request, userId);
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

}
