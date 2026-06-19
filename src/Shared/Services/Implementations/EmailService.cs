using MailKit.Net.Smtp;
using EmailScheduler.src.Shared.Services.Interfaces;
using EmailScheduler.src.Shared.Services.Settings;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailScheduler.src.Shared.Services.Implementations;

public class EmailService(IOptions<SmtpSettings> settings) : IEmailService
{

    private readonly SmtpSettings smtpSettings = settings.Value;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = body
        };


        using var client = new SmtpClient();
        await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        
        if (!string.IsNullOrEmpty(smtpSettings.Username))
        {
            await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
        }

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}