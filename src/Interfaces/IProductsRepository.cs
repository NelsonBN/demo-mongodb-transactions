using Demo.MongoDB.Transactions.Models;

namespace Demo.MongoDB.Transactions.Interfaces;

public interface IProductsRepository
{
    Task AddAsync(Product entity);

    Task<IEnumerable<Product>> GetAllAsync();
}
