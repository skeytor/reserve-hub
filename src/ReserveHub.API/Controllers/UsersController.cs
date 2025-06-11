using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.UseCases.Users.Create;

namespace ReserveHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType<BadRequest<ValidationProblemDetails>>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<Guid>, BadRequest<ValidationProblemDetails>>> RegisterAsync(
        [FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value) 
            : TypedResults.BadRequest(result.ToProblemDetails());
    }
}
