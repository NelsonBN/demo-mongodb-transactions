using Demo.MongoDB.Transactions.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Demo.MongoDB.Transactions.DataBase;

public class MongoDBContext : IMongoDBContext
{
    private readonly IMongoDatabase _database;

    private MongoClient _client { get; set; }

    private IClientSessionHandle _transaction { get; set; } = default!;


    static MongoDBContext() // Changes Id format from "ObjectID with 96 bits" to "GUID with 128 bits"
        => BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));


    public MongoDBContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var mongoUrl = new MongoUrl(connectionString);

        var mongoClientSettings = new MongoClientSettings();
        mongoClientSettings.Server = mongoUrl.Server;
        mongoClientSettings.Credential = MongoCredential.CreateCredential(null, mongoUrl.Username, mongoUrl.Password);

        _client = new MongoClient(mongoClientSettings);

        _database = _client.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collection)
        => _database.GetCollection<T>(collection);


    public async Task<IClientSessionHandle> GetTransaction()
    {
        if(_transaction == null)
        {
            _transaction = await _client.StartSessionAsync();
            try
            {
                _transaction.StartTransaction();
            }
            catch
            {
                await _transaction.AbortTransactionAsync();
                throw;
            }
        }

        return _transaction;
    }


    public void Dispose()
    {
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}
