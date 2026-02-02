using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IEventRepository
{
    Task AddAsync(Event item, CancellationToken token);
    Task<Event?> GetByIdAsync(Guid id, CancellationToken token);
    Task UpdateAsync(Event item, CancellationToken token);
}