using MediatR;

namespace EmailScheduler.src.Shared.Notifications;

public record EmailScheduleTrigger(
    Guid ScheduleId
) : INotification;