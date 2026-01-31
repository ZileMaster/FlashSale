using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _repository;

    public CreateEventCommandHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event(request.Name, request.TotalTickets);
        
        await _repository.AddAsync(newEvent, cancellationToken);

        return newEvent.Id;
    }
}