using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Commands.PurchaseTicket;

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

    [HttpPost("purchase")]
    public async Task<IActionResult> Purchase(PurchaseTicketCommand command)
    {
        var success = await _mediator.Send(command);
        if (!success)
        {
            return BadRequest("Tickets sold out or event not found.");
        }

        return Ok("Tickets purchased successfully.");
    }
}