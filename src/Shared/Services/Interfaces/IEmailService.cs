using EmailScheduler.src.Shared.Services.Settings;

namespace EmailScheduler.src.Shared.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, SmtpSettings settings);
}