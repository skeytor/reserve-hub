using System.ComponentModel.DataAnnotations;

namespace ReserveHub.Application.UseCases.Reservations.Create;

public sealed record CreateReservationRequest(
    [Required] int SpaceId, 
    [Required] DateTime StartTime, 
    [Required] DateTime EndTime,
    [MaxLength(200)] string? Notes);