using Demo.MongoDB.Transactions.Interfaces;
using Demo.MongoDB.Transactions.Models;
using MongoDB.Driver;

namespace Demo.MongoDB.Transactions.DataBase;

public class ProductsRepository : IProductsRepository
{
    private const string COLLECTION_NAME = nameof(Product);

    private readonly IMongoDBContext _context;
    private readonly IMongoCollection<Product> _collection;


    public ProductsRepository(IMongoDBContext context)
    {
        _context = context;
        _collection = _context.GetCollection<Product>(COLLECTION_NAME);
    }


    public async Task AddAsync(Product entity)
    {
        var transaction = await _context.GetTransaction();
        await _collection.InsertOneAsync(transaction, entity);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => (await _collection
            .FindAsync(Builders<Product>.Filter.Empty))
            .ToList();
}
