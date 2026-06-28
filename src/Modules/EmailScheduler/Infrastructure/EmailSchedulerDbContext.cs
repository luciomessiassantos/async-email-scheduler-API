using EmailScheduler.src.Modules.EmailScheduler.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmailScheduler.src.Modules.EmailScheduler.Infrastructure;

public class EmailSchedulerDbContext : DbContext
{

    public EmailSchedulerDbContext(DbContextOptions<EmailSchedulerDbContext> options) : base(options)
    {
        
    }

    public DbSet<EmailSchedulerEntity> EmailSchedulers { get; set; }
    public DbSet<SmtpClientSettings> SmtpClientSettingsSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<EmailSchedulerEntity>(e =>
        {
            e.HasKey(e => e.Id);

            e.Property(e => e.TriggerDate)
            .HasColumnType("timestamp with time zone");

            e.HasOne(e => e.SmtpClientSettings)
            .WithMany(c => c.EmailSchedulers)
            .HasForeignKey(c => c.SmtpClientSettingsId);
            
        });
        

        modelBuilder.Entity<SmtpClientSettings>(e =>
        {
            e.HasKey(e => e.Id);
            e.HasMany(e => e.EmailSchedulers)
            .WithOne(s => s.SmtpClientSettings)
            .HasForeignKey(s => s.SmtpClientSettingsId);

        });


    

        base.OnModelCreating(modelBuilder);
    }

}