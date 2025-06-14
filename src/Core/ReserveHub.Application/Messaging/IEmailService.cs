namespace ReserveHub.Application.Messaging;

public interface IEmailService
{
    Task SendEmail(string to, string subject, string body);
}
