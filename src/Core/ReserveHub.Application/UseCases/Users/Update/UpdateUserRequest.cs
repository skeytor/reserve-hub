using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Users.Update;

public sealed record UpdateUserRequest(
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress, MaxLength(100)] string Email);
