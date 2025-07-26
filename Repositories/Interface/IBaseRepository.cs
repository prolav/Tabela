using System.Linq.Expressions;

namespace Tabela.Repositories;

public interface IBaseRepository<T>
{
    void Update(T obj);
    void Insert(T obj);
    void InsertOrReplace(T obj);
    void Delete(T obj);
    T GetById(Guid id);
    List<T> GetAll();
    void DeleteAll();
}
