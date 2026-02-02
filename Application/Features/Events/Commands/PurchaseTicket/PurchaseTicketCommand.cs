using MediatR;

namespace Application.Features.Events.Commands.PurchaseTicket;

public record PurchaseTicketCommand(Guid EventId, int Quantity) : IRequest<bool>;