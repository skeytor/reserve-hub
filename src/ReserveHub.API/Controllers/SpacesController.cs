using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.Services;
using ReserveHub.Application.UseCases.Spaces.Create;
using ReserveHub.Application.UseCases.Spaces.GetAvailableSpaces;
using SharedKernel;

namespace ReserveHub.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SpacesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<int>, BadRequest<ValidationProblemDetails>>> Create(
        [FromBody] CreateSpaceRequest request)
    {
        var command = new CreateSpaceCommand(request);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.ToProblemDetails());
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType<PagedList<SpaceResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<PagedList<SpaceResponse>>, BadRequest<ValidationProblemDetails>>> GetAvailableSpaces(
        [FromQuery] PaginationParams paginationParams,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var query = new GetAvailableSpacesQuery(paginationParams, startDate, endDate);
        var result = await sender.Send(query);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.BadRequest(result.ToProblemDetails());
    }
}
