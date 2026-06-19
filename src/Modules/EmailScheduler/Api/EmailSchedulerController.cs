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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await mediator.Send(new GetAllSchedulesQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {

        GetScheduleByIdQuery query = new(id);
        return Ok(await mediator.Send(query));
    }

}