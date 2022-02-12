namespace Demo.MongoDB.Transactions.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
    Task Rollback();
}
