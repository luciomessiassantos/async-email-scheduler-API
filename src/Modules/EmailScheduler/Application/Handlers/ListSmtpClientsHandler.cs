using EmailScheduler.src.Modules.EmailScheduler.Api;
using EmailScheduler.src.Modules.EmailScheduler.Application.Queries;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class ListSmtpClientsHandler(
    EmailSchedulerDbContext context
) : IRequestHandler<ListSmtpClientsQuery, IEnumerable<SmtpClientSettingsDto>>
{
    public async Task<IEnumerable<SmtpClientSettingsDto>> Handle(ListSmtpClientsQuery request, CancellationToken cancellationToken)
    {
        return await context.SmtpClientSettingsSet
            .Select(s => new SmtpClientSettingsDto(s.Id, s.Server, s.Port, s.SenderName, s.SenderEmail, s.Username))
            .ToListAsync(cancellationToken);
    }
}