using EmailScheduler.src.Modules.EmailScheduler.Application.Queries;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class ListSchedulesHandler(
    EmailSchedulerDbContext context
) : IRequestHandler<ListSchedulesQuery, IEnumerable<EmailSchedulerEntity>>
{
    public async Task<IEnumerable<EmailSchedulerEntity>> Handle(ListSchedulesQuery request, CancellationToken cancellationToken)
    {
        return await context.EmailSchedulers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }
}