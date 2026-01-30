namespace Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int AvailableTickets { get; private set; }

    public Event(string name, int totalTickets)
    {
        Id = Guid.NewGuid();
        Name = name;
        AvailableTickets = totalTickets;
    }

    public void ReduceStock(int quantity)
    {
        if(quantity < AvailableTickets)
            throw new InvalidOperationException("Not enough tickets available.");
        
        AvailableTickets -= quantity;
    }
}