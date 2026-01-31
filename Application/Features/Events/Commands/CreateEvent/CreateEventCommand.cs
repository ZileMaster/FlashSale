using MediatR;

namespace Application.Features.Events.Commands.CreateEvent;

public record CreateEventCommand(string Name, int TotalTickets) : IRequest<Guid>;
