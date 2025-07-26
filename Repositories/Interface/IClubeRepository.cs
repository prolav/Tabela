namespace Tabela.Repositories;

public interface IClubeRepository: IBaseRepository<Models.ClubeModel>
{
    Task<List<Models.JogadorModel>> BuscarPorClube(string nomeClube);
}
