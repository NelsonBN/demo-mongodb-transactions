using MongoDB.Driver;

namespace Demo.MongoDB.Transactions.Interfaces;

public interface IMongoDBContext : IDisposable
{
    Task<IClientSessionHandle> GetTransaction();

    IMongoCollection<T> GetCollection<T>(string collection);
}
