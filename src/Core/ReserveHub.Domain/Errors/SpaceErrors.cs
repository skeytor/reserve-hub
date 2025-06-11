using SharedKernel.Results;

namespace ReserveHub.Domain.Errors;

public static class SpaceErrors
{
    public static Error SpaceAlreadyExists =>
        Error.Failure("Space.SpaceAlreadyExists", "A space with the same name already exists.");
}
