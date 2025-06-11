using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.UseCases.Spaces.Create;

namespace ReserveHub.API.Controllers;

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
}
