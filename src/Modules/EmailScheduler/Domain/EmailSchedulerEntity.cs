using System.Text.Json.Serialization;

namespace EmailScheduler.src.Modules.EmailScheduler.Domain;

public class EmailSchedulerEntity
{
    public Guid Id { get; set; }

    public required string Email { get; set; }

    public required string Subject { get; set; }

    public required string Body { get; set; }

    public DateTimeOffset TriggerDate { get; set; }

    public SchedulerStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; }

    public Guid SmtpClientSettingsId { get; set; }

    public SmtpClientSettings SmtpClientSettings { get; set; } = null!;

    public EmailSchedulerEntity()
    {
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Cancel() => Status = SchedulerStatus.Cancelled;
    public void Trigger() => Status = SchedulerStatus.Triggered;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SchedulerStatus
{
    Pending,
    Cancelled,
    Triggered
}
