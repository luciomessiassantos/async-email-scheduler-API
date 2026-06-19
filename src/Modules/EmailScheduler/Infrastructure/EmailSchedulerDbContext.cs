using EmailScheduler.src.Modules.EmailScheduler.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Infrastructure;

public class EmailSchedulerDbContext : DbContext
{

    public EmailSchedulerDbContext(DbContextOptions<EmailSchedulerDbContext> options) : base(options)
    {
        
    }

    public DbSet<EmailSchedulerEntity> EmailSchedulers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailSchedulerEntity>()
        .HasKey(e => e.Id);

        modelBuilder.Entity<EmailSchedulerEntity>()
        .Property(e => e.TriggerDate)
        .HasColumnType("timestamp with time zone");

        base.OnModelCreating(modelBuilder);
    }

}