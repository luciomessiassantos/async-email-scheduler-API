using EmailScheduler.src.Modules.EmailScheduler.Domain;
using EmailScheduler.src.Modules.EmailScheduler.Domain.Dto;
using MediatR;

namespace EmailScheduler.src.Modules.EmailScheduler.Application.Queries;

public record ListSmtpClientsQuery() : IRequest<IEnumerable<SmtpClientSettingsDto>>;