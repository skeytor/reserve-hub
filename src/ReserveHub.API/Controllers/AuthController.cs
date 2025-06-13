using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReserveHub.API.Extensions;
using ReserveHub.Application.UseCases.Auth.Login;

namespace ReserveHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<Results<Ok<AccessTokenResponse>, BadRequest<ValidationProblemDetails>>> Login(
        [FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request);
        var result = await sender.Send(command);
        return result.IsSuccess
            ? TypedResults.Ok(new AccessTokenResponse { 
                AccessToken = result.Value, 
                ExpiresIn = 10000, 
                RefreshToken = Guid.NewGuid().ToString()})
            : TypedResults.BadRequest(result.ToProblemDetails());
    }
}
