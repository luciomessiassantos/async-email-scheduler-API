using EmailScheduler.src.Shared.Notifications;
using MediatR;
using Quartz;

namespace EmailScheduler.src.Shared.Jobs;

[DisallowConcurrentExecution] 
public class ProcessEmailSchedule(
    ILogger<ProcessEmailSchedule> logger,
    IServiceScopeFactory scopeFactory
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.MergedJobDataMap;
        string? scheduleId = dataMap.GetString("ScheduleId");

        if (scheduleId == null)
        {
            logger.LogWarning($"Id - {scheduleId} nulo");
            throw new Exception("Schedule não encontrado");
        }
        

        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Publish(new EmailScheduleTrigger(Guid.Parse(scheduleId)));
    }
}