using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmailScheduler.src.Modules.EmailScheduler.Infrastructure;

public class EmailSchedulerDbContextFactory : IDesignTimeDbContextFactory<EmailSchedulerDbContext>
{
    public EmailSchedulerDbContext CreateDbContext(string[] args)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EmailSchedulerDbContext>();
        var connectionString = configuration.GetConnectionString("Default");

        optionsBuilder.UseNpgsql(connectionString);

        return new EmailSchedulerDbContext(optionsBuilder.Options);
    }
}