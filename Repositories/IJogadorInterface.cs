namespace Tabela.Repositories;

public interface IJogadorRepository : IBaseRepository<Models.JogadorModel>
{
    Task<List<Models.JogadorModel>> BuscarPorTime(string nomeTime);
}