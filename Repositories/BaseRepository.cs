using System.Linq.Expressions;
using SQLite;

namespace Tabela.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : new()
{
    protected readonly SQLiteAsyncConnection _db;

    public BaseRepository(SQLiteAsyncConnection db)
    {
        _db = db;
        _db.CreateTableAsync<T>().Wait();
    }

    public Task<List<T>> GetAllAsync() =>
        _db.Table<T>().ToListAsync();

    public Task<T> GetByIdAsync(int id) =>
        _db.FindAsync<T>(id);

    public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        _db.Table<T>().Where(predicate).ToListAsync();

    public Task<int> AddAsync(T entity) =>
        _db.InsertAsync(entity);

    public Task<int> UpdateAsync(T entity) =>
        _db.UpdateAsync(entity);

    public Task<int> DeleteAsync(T entity) =>
        _db.DeleteAsync(entity);
}
