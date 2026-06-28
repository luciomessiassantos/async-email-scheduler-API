namespace EmailScheduler.src.Modules.EmailScheduler.Domain;

public class SmtpClientSettings
{
    public Guid Id { get; set; }
    
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? Password { get; set; }

    public List<EmailSchedulerEntity> EmailSchedulers { get; set; } = [];
    
}

