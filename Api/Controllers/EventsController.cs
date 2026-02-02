using MediatR; 
using Microsoft.AspNetCore.Mvc; 
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Commands.PurchaseTicket;
using Domain.Events;
using MassTransit;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    public readonly IMediator _mediator;
    private readonly ITopicProducer<TicketPurchaseRequest> _producer;

    public EventsController(IMediator mediator,  ITopicProducer<TicketPurchaseRequest> producer)
    {
        _mediator = mediator;
        _producer = producer;
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
        await _producer.Produce(new TicketPurchaseRequest
        {
            EventId =  command.EventId,
            Quantity = command.Quantity,
            UserId = Guid.NewGuid(), 
            Timestamp = DateTime.UtcNow
        });

        return Accepted("Request received! You will get an email shortly.");
    }
}