using Application.Common.Interfaces;
using Domain.Events;
using MassTransit;

namespace Application.Features.Events.Consumers;

public class TicketPurchaseConsumer : IConsumer<TicketPurchaseRequest>
{
    private readonly IEventRepository _eventRepository;

    public TicketPurchaseConsumer(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Consume(ConsumeContext<TicketPurchaseRequest> context)
    {
        Console.WriteLine(">>>>>>>>>>>>>>>>> I RECEIVED A MESSAGE!!!! <<<<<<<<<<<<<<<<<");
        
        var request = context.Message;
        
        var eventItem = _eventRepository.GetByIdAsync(request.EventId, context.CancellationToken).Result;

        if (eventItem == null)
            return;

        try
        {
            eventItem.ReduceStock(request.Quantity);

            await _eventRepository.UpdateAsync(eventItem, context.CancellationToken);

            Console.WriteLine($"[Success] Sold {request.Quantity} tickets for {eventItem.Name}!");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine($"[Sold Out] User {request.UserId} tried to buy tickets for {eventItem.Name}!");
        }
    }
    
}