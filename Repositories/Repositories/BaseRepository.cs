
using SQLite;
using Tabela.Data;

namespace Tabela.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : new()
    {
        private SQLiteConnection _conn;
        public BaseRepository()
        {
            _conn = DbContext.Current.Connection;
        }

        public void Delete(T obj)
        {
            try
            {
                if (obj is null)
                    throw new Exception("Objeto não pode ser nulo para esta acão deletar");
                _conn.Delete(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteAll()
        {
            try
            {
                _conn.DeleteAll<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Insert(T obj)
        {
            try
            {
                if (obj is null)
                    throw new Exception("Objeto não pode ser nulo para esta acão de inserir");
                 _conn.Insert(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(T obj)
        {
            try
            {
                if (obj is null)
                    throw new Exception("Objeto não pode ser nulo para esta acão de update");
                 _conn.Update(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new Exception("Guid inválido");

                return _conn.Find<T>(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return  _conn.Table<T>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertOrReplace(T obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Objeto não pode ser nulo para esta acão de inserir");
                _conn.InsertOrReplace(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
