using Tabela.Models;

namespace Tabela.Repositories.Interface
{
    public interface IClassificacaoRepository : IBaseRepository<Models.ClassificacaoModel>
    {
        void SalvarClassificacaoByPartida(List<PartidaModel> listaPartida);
    }
}