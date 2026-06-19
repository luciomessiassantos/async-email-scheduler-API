using EmailScheduler.src.Modules.EmailScheduler.Application.Commands;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Infrastructure;
using EmailScheduler.src.Shared.Jobs;
using MediatR;
using Quartz;
using Quartz.Impl;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Handlers;

public class CreateEmailScheduleHandler(
    EmailSchedulerDbContext context,
    ISchedulerFactory factory
) : IRequestHandler<CreateEmailScheduler, EmailSchedulerEntity>
{
    public async Task<EmailSchedulerEntity> Handle(CreateEmailScheduler request, CancellationToken cancellationToken)
    {

        var timezone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

        DateTime localDateTime = request.TriggerDate.ToDateTime(request.TriggerTime);

        DateTimeOffset utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, timezone);

        const string QUARTZ_GROUP = "EmailScheduleGroup";
        var scheduler = await factory.GetScheduler(cancellationToken);
        // criar a entidade
        var entity = new EmailSchedulerEntity()
        {
            Email = request.Email,
            Subject = request.Subject,
            Body = request.Body,
            TriggerDate = utcDateTime,
            Status = SchedulerStatus.Pending
        };

        // programar os jobs

        var saved = await context.AddAsync(entity, cancellationToken);
        var e = saved.Entity;

        var jobData = new JobDataMap
        {
            { "ScheduleId", e.Id }
        };

        var emailJob = JobBuilder.Create<ProcessEmailSchedule>()
                        .WithIdentity($"schedule-{e.Id}", QUARTZ_GROUP)
                        .UsingJobData(jobData)
                        .Build();

        var trigger = TriggerBuilder.Create()
                        .WithIdentity($"trigger-{e.Id}", QUARTZ_GROUP)
                        .StartAt(e.TriggerDate)
                        .Build();
        await scheduler.ScheduleJob(emailJob, trigger, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        return e;
    }
}