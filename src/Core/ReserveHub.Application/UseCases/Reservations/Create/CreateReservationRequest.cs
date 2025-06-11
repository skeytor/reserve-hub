using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Reservations.Create;

public sealed record CreateReservationRequest(
    [Required] Guid UserId, 
    [Required] Guid SpaceId, 
    [Required] DateTime StartTime, 
    [Required] DateTime EndTime,
    [MaxLength(200)] string? Notes);