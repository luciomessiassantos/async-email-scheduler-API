using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using EmailScheduler.src.Shared.Notifications.Triggers;
using EmailScheduler.src.Shared.Services.Interfaces;
using EmailScheduler.src.Shared.Services.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Shared.Notifications.Handlers;

public class TriggerEmailScheduleNotification(
    EmailSchedulerDbContext context,
    IEmailService emailService,
    ILogger<TriggerEmailScheduleNotification> logger
) : INotificationHandler<EmailScheduleTrigger>
{
    public async Task Handle(EmailScheduleTrigger notification, CancellationToken cancellationToken)
    {
        // Aqui será feito a procura do agendamento pelo Id
        var schedule = await context.EmailSchedulers
        .Include(e => e.SmtpClientSettings)
        .AsTracking()
        .FirstOrDefaultAsync(e => e.Id == notification.ScheduleId, cancellationToken)
            ?? throw new Exception("Agendamento não encontrado");
        // e a chamada do serviço e envio de email

        try
        {
            await emailService.SendEmailAsync(
                schedule.Email,
                schedule.Subject,
                schedule.Body,
                new SmtpSettings // uso de client registrado
                {
                    Username = schedule.SmtpClientSettings.Username,
                    Server = schedule.SmtpClientSettings.Server,
                    SenderName = schedule.SmtpClientSettings.SenderName,
                    SenderEmail = schedule.SmtpClientSettings.SenderEmail,
                    Password = schedule.SmtpClientSettings.Password ?? ""
                }
            );

            schedule.Trigger();
            logger.LogInformation($"Testing OK - {schedule.Status}");
        } catch(Exception e)
        {
            logger.LogError(e.Message);
            schedule.Cancel();
        }

        logger.LogInformation("Testing OK - Saved");
        logger.LogInformation($"Testing OK3 - {schedule.Status}");
        await context.SaveChangesAsync(cancellationToken);
        
    }
}