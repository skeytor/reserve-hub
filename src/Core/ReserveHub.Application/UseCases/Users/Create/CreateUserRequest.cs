using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Users.Create;

public sealed record CreateUserRequest(
    [Required, MaxLength(50)] string FirstName, 
    [Required, MaxLength(50)] string LastName, 
    [Required, EmailAddress, MaxLength(80)] string Email,
    [Required, DataType(DataType.Password)] string Password);