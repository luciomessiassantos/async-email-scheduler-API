using EmailScheduler.src.Modules.EmailScheduler.Application.Commands;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class RegisterSmtpClientSettingsHandler(
    EmailSchedulerDbContext context
) : IRequestHandler<RegisterSmtpClientSettingsCommand, SmtpClientSettingsDto>
{
    public async Task<SmtpClientSettingsDto> Handle(RegisterSmtpClientSettingsCommand request, CancellationToken cancellationToken)
    {
        var entity = new SmtpClientSettings
        {
            Username = request.Username,
            Server = request.Server,
            SenderName = request.SenderName,
            SenderEmail = request.SenderEmail,
            Port = request.Port,
            Password = request.Password
        };

        var saved = await context.SmtpClientSettingsSet.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new SmtpClientSettingsDto(
            saved.Entity.Id,
            saved.Entity.Server,
            saved.Entity.Port,
            saved.Entity.SenderName,
            saved.Entity.SenderEmail,
            saved.Entity.Username
        );
    }
}