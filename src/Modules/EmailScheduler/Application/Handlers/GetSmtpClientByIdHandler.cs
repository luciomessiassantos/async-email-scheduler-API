using EmailScheduler.src.Modules.EmailScheduler.Application.Queries;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class GetSmtpClientByIdHandler(
    EmailSchedulerDbContext context
) : IRequestHandler<GetSmtpClientSettingsByIdQuery, SmtpClientSettingsDto?>
{
    public async Task<SmtpClientSettingsDto?> Handle(GetSmtpClientSettingsByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.SmtpClientSettingsSet
            .Select(s => new SmtpClientSettingsDto(s.Id, s.Server, s.Port, s.SenderName, s.SenderEmail, s.Username))
            .FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);
    }
}