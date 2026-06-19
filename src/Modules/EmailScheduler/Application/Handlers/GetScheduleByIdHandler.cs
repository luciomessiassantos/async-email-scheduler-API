using EmailScheduler.src.Modules.EmailScheduler.Application.Queries;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class GetScheduleByIdHandler(
    EmailSchedulerDbContext context
) : IRequestHandler<GetScheduleByIdQuery, EmailSchedulerEntity?>
{
    public async Task<EmailSchedulerEntity?> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.EmailSchedulers.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
    }
}