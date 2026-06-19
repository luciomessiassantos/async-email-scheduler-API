using EmailScheduler.src.Modules.EmailScheduler.Domain;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Commands;

public record CreateEmailScheduler(
    string Email,
    string Subject,
    string Body,
    DateOnly TriggerDate,
    TimeOnly TriggerTime
) : IRequest<EmailSchedulerEntity>;