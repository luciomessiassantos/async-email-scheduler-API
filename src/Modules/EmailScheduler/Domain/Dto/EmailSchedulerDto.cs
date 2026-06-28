namespace EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;

public record EmailSchedulerDto(
    Guid Id,
    string Email,
    string Subject,
    string Body,
    DateTimeOffset TriggerDate,
    SchedulerStatus Status,
    DateTimeOffset CreatedAt,
    Guid SmtpClientSettingsId
);