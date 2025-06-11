using SharedKernel.Results;

namespace ReserveHub.Application.Extensions;

public static class ReservationErrors
{
    public static Error DateOutOfRange => 
        Error.Failure("Reservation.DateOutOfRange", "Start time must be before end time.");
    public static Error SpaceNotAvailable => 
        Error.Failure("Reservation.SpaceNotAvailable", "The space is not available for the selected time.");
}
