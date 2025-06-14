namespace ReserveHub.Application.Providers;

public interface IGoogleCalendarProvider
{
    Task CreateEvent(
        string calendarId, 
        string summary, 
        string description, 
        DateTime startTime, 
        DateTime endTime, 
        string location);
}
