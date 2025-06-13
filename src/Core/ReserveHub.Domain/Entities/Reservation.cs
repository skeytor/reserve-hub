using ReserveHub.Domain.Enums;
using SharedKernel.Abstractions;

namespace ReserveHub.Domain.Entities;

public class Reservation : BaseEntity<Guid>
{
    public Guid UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public int SpaceId { get; set; }
    public Space Space { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public string Notes { get; set; } = string.Empty;
    //public ICollection<NotificationToken> Notifications { get; set; } = [];
}
