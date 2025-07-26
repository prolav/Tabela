using SQLite;
using Tabela.Models;

namespace Tabela.Data;

public class DbContext
{
    private static DbContext _lazy;
    private static SQLiteConnection _sqlConnection;
    private const string _dbName = "GateScore.db3";

    public static DbContext Current
    {
        get
        {
            if (_lazy == null)
                _lazy = new DbContext();
            return _lazy;
        }
    }

    private DbContext()
    {
        _sqlConnection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, _dbName));
        _sqlConnection.CreateTable<CampeonatoModel>();
        _sqlConnection.CreateTable<ClubeModel>();
        _sqlConnection.CreateTable<FaseModel>();
        _sqlConnection.CreateTable<GrupoModel>();
        _sqlConnection.CreateTable<JogadorModel>();
        _sqlConnection.CreateTable<PartidaModel>();
        _sqlConnection.CreateTable<TimeModel>();
        _sqlConnection.CreateTable<UsuarioModel>();
    }

    public SQLiteConnection Connection
    {
        get { return _sqlConnection; }
        set { _sqlConnection = value; }
    }
}
