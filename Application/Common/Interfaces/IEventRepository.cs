using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IEventRepository
{
    Task AddAsync(Event item, CancellationToken token);
}