using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Events.Commands.PurchaseTicket;

public class PurchaseTicketCommandHandler : IRequestHandler<PurchaseTicketCommand, bool>
{
    private readonly IEventRepository _eventRepository;

    public PurchaseTicketCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task<bool> Handle(PurchaseTicketCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);

        if (eventItem == null)
            return false;

        try
        {
            eventItem.ReduceStock(request.Quantity);
        }
        catch (InvalidOperationException)
        {
            return false;
        }
        
        await _eventRepository.UpdateAsync(eventItem, cancellationToken);
        return true;
    }
}