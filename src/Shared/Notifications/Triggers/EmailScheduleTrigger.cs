using MediatR;

namespace EmailScheduler.src.Shared.Notifications.Triggers;

public record EmailScheduleTrigger(
    Guid ScheduleId
) : INotification;