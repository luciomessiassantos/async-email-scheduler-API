using EmailScheduler.src.Modules.EmailScheduler.Domain;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Queries;

public record GetAllSchedulesQuery() : IRequest<IEnumerable<EmailSchedulerEntity>>;