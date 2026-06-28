using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Commands;

public record RegisterSmtpClientSettingsCommand(
    string Server,
    int Port,
    string SenderName,
    string SenderEmail,
    string Username,
    string? Password
) : IRequest<SmtpClientSettingsDto>;