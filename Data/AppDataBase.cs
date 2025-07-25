using SQLite;
using Tabela.Models;

namespace Tabela.Data;

public class AppDatabase
{
    private static SQLiteAsyncConnection _connection;
        public static SQLiteAsyncConnection GetConnection()
        {
            if (_connection == null)
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db3");
                _connection = new SQLiteAsyncConnection(dbPath);
            }

            return _connection;
        }

}