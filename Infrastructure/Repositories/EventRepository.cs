using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EventRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Event item, CancellationToken token)
    {
        await _dbContext.Events.AddAsync(item, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbContext.Events.FindAsync(new object[] { id }, token);
    }

    public Task UpdateAsync(Event item, CancellationToken token)
    {
        _dbContext.Events.Update(item);
        return _dbContext.SaveChangesAsync(token);
    }
}