namespace ReserveHub.Application.UseCases.Users.GetProfile;

public sealed record UserResponse(Guid Id, string FirstName, string LastName);
