using Demo.MongoDB.Transactions.Interfaces;

namespace Demo.MongoDB.Transactions.DataBase;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoDBContext _context;

    public UnitOfWork(IMongoDBContext context)
        => _context = context;

    public async Task<bool> Commit()
    {
        var transaction = await _context.GetTransaction();
        await transaction.CommitTransactionAsync();

        return true;
    }


    public async Task Rollback()
    {
        var transaction = await _context.GetTransaction();
        await transaction.AbortTransactionAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            _context.Dispose();
        }
    }
}
