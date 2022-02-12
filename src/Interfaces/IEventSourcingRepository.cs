using Demo.MongoDB.Transactions.Models;

namespace Demo.MongoDB.Transactions.Interfaces;

public interface IEventSourcingRepository
{
    Task AddAsync(EventSourcing entity);

    Task<IEnumerable<EventSourcing>> GetAllAsync();
}
