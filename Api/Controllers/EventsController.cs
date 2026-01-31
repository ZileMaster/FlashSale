using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using Application.Features.Events.Commands.CreateEvent;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    public readonly IMediator _mediator;
    
    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}