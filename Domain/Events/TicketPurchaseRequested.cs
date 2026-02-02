namespace Domain.Events;

public record TicketPurchaseRequest
{
    public Guid EventId { get; init; }
    public int Quantity { get; init; }
    public Guid UserId { get; init; }
    public DateTime Timestamp { get; init; }
}