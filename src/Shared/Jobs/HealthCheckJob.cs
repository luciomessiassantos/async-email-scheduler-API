using Quartz;

namespace EmailScheduler.src.Shared.Jobs;

public class HealthCheckJob(
    ILogger<HealthCheckJob> logger
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Testing");
    }
}