using Microsoft.AspNetCore.Identity;
using ReserveHub.Application.Handlers;
using ReserveHub.Application.Providers;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Errors;
using ReserveHub.Domain.Repositories;
using SharedKernel.Results;
using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Auth.Login;

internal class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenProvider tokenProvider,
    IPasswordHasher<User> passwordHasher) : ICommandHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByEmailAsync(request.Credentials.Email);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }
        PasswordVerificationResult passwordVerification = passwordHasher
            .VerifyHashedPassword(user, user.PasswordHash, request.Credentials.Password);
        if (passwordVerification is PasswordVerificationResult.Failed)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }
        string token = tokenProvider.GenerateToken(user);
        return token;
    }
}
public sealed record LoginCommand(LoginRequest Credentials) : ICommand<string>;
public sealed record LoginRequest(
    [Required, EmailAddress] string Email, 
    [Required, DataType(DataType.Password)] string Password);