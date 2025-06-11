using SharedKernel.Results;

namespace ReserveHub.Domain.Errors;

public static class UserErrors
{
    public static Error NotFound => Error.NotFound(
        "User.NotFound",
        "The user with the specified email address was not found."
    );
    public static Error InvalidCredentials => Error.Failure(
        "User.InvalidCredentials",
        "Invalid credentials");
    public static Error IsAlreadyBy(string email) => Error.Failure(
        "User.EmailIsAlready",
        $"The email {email} is already");
}
