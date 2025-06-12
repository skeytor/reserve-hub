using ReserveHub.Application.UseCases.Spaces.GetAvailableSpaces;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Application.Extensions;

internal static class SpaceExtensions
{
    internal static SpaceResponse ToResponse(this Space source)
        => new(
            source.Id,
            source.Name,
            source.Description,
            source.IsActive);
    internal static IReadOnlyList<SpaceResponse> ToResponse(this IReadOnlyList<Space> source)
        => [.. source.Select(ToResponse)];
}
