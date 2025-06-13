namespace ReserveHub.Domain.Entities;

public class NotificationToken
{
    public Guid Id { get; set; }
    public Guid ReservationId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ExpiredOnUtc { get; set; }
    public Reservation Reservation { get; set; } = null!;
}
