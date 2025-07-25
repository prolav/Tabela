using SQLite;

namespace Tabela.Repositories;

public class ClubeRepository : BaseRepository<Models.ClubeModel>, IClubeRepository
{
    public ClubeRepository(SQLiteAsyncConnection db) : base(db)
    {
        
    }

    public Task<List<Models.JogadorModel>> BuscarPorClube(string nomeClube)
    {
        //return _db.Table<Models.ClubeModel>().Where(j.Nome_clube =>  == nomeTime).ToListAsync();
        return null;
    }
}
