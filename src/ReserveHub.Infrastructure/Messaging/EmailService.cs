using FluentEmail.Core;
using ReserveHub.Application.Messaging;

namespace ReserveHub.Infrastructure.Messaging;

public sealed class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    public async Task SendEmail(string to, string subject, string body) 
        => await fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body, isHtml: true)
            .SendAsync();
}
