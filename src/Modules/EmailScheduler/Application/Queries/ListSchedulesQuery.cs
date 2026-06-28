using EmailScheduler.src.Modules.EmailScheduler.Domain;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Queries;

public record ListSchedulesQuery(
    
) : IRequest<IEnumerable<EmailSchedulerEntity>>;