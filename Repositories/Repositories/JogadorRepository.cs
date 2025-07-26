using SQLite;


namespace Tabela.Repositories;

public class JogadorRepository : BaseRepository<Models.JogadorModel>, IJogadorRepository
{
  
    public Task<List<Models.JogadorModel>> BuscarPorTime(string nomeTime)
    {
        //return _db.Table<Models.JogadorModel>().Where(j => j.(Time.Time_Nome) == nomeTime).ToListAsync();
        return null;
    }
}
