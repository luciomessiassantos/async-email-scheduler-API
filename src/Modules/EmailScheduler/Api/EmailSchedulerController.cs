using EmailScheduler.src.Modules.EmailScheduler.Application.Commands;
using EmailScheduler.src.Modules.EmailScheduler.Application.Queries;
using EmailScheduler.src.Modules.EmailScheduler.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailScheduler.src.Modules.EmailScheduler.Api;

[ApiController]
[Route("api/scheduler")]
public class EmailSchedulerController(
    IMediator mediator
) : ControllerBase
{
    

    [HttpPost]
    public async Task<IActionResult> CreateEmailScheduler([FromBody] CreateEmailScheduler command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> ListSchedules()
    {
        return Ok(await mediator.Send(new ListSchedulesQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {

        GetScheduleByIdQuery query = new(id);
        return Ok(await mediator.Send(query));
    }

    [HttpPost("/smtp")]
    public async Task<IActionResult> RegisterSmtpClientSettings([FromBody] RegisterSmtpClientSettingsCommand command)
    {
        var result = await mediator.Send(command);

        return CreatedAtAction(nameof(GetSmtpClientSettingsById), new { ClientId = result.Id }, result);
    }

    [HttpGet("/smtp")]
    public async Task<IActionResult> ListSmtpClientSettings()
    {
        var result = await mediator.Send(new ListSmtpClientsQuery());

        return Ok(result);
    }

    [HttpPost("/smtp/{CLientId:guid}")]
    public async Task<IActionResult> GetSmtpClientSettingsById([FromRoute] Guid ClientId)
    {
        var result = await mediator.Send(new GetSmtpClientSettingsByIdQuery(ClientId));

        return Ok(result);
    }

    

}