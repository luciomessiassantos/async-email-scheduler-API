namespace EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;

public record SmtpClientSettingsDto(
    Guid Id,
    string Server,
    int Port,
    string SenderName,
    string SenderEmail,
    string Username
);