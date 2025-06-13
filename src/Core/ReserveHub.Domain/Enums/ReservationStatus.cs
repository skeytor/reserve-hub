using System.Text.Json.Serialization;

namespace ReserveHub.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReservationStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Completed
}
