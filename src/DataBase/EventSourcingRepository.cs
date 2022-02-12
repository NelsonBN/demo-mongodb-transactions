using Demo.MongoDB.Transactions.Interfaces;
using Demo.MongoDB.Transactions.Models;
using MongoDB.Driver;

namespace Demo.MongoDB.Transactions.DataBase;

public class EventSourcingRepository : IEventSourcingRepository
{
    private const string COLLECTION_NAME = nameof(EventSourcing);

    private readonly IMongoDBContext _context;
    private readonly IMongoCollection<EventSourcing> _collection;

    public EventSourcingRepository(IMongoDBContext context)
    {
        _context = context;
        _collection = _context.GetCollection<EventSourcing>(COLLECTION_NAME);
    }

    public async Task AddAsync(EventSourcing entity)
    {
        var transaction = await _context.GetTransaction();
        await _collection.InsertOneAsync(transaction, entity);
    }

    public async Task<IEnumerable<EventSourcing>> GetAllAsync()
        => (await _collection
            .FindAsync(Builders<EventSourcing>.Filter.Empty))
            .ToList();
}
