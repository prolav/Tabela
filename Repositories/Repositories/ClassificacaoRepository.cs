using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.Models;
using Tabela.Repositories.Interface;

namespace Tabela.Repositories.Repositories
{
    public class ClassificacaoRepository : BaseRepository<Models.ClassificacaoModel>, IClassificacaoRepository
    {
        public void SalvarClassificacaoByPartida(List<PartidaModel> listaPartida)
        {
            var timeRepository = new TimeRepository();
            var classificacaoRepository = new ClassificacaoRepository();
            var partidaRepository = new PartidaRepository();
            foreach (var partida in listaPartida)
            {
                bool jaExisteCasa = classificacaoRepository
                    .GetAll()
                    .Any(x => x.Classificacao_PartidaId == partida.Id && x.Classificacao_TimeId == partida.TimeCasaId);

                bool jaExisteFora = classificacaoRepository
                    .GetAll()
                    .Any(x => x.Classificacao_PartidaId == partida.Id && x.Classificacao_TimeId == partida.TimeForaId);

                if (!jaExisteCasa && !jaExisteFora)
                {
                    var classificacaoCasa = new ClassificacaoModel
                    {
                        Classificacao_PartidaId = partida.Id,
                        Classificacao_Campo = partida.Partida_NumeroCampo,
                        Classificacao_TimeId = partida.TimeCasaId,
                        Time = timeRepository.GetById(partida.TimeCasaId),
                        Classificacao_Vitoria = partida.IsTimeCasaVencedor ? 1 : 0,
                        Classificacao_Derrota = partida.IsTimeForaVencedor ? 1 : 0,
                        Classificacao_PontosPro = partida.Partida_PontosCasa,
                        Classificacao_PontosContra = partida.Partida_PontosFora,
                        Classificacao_QtdeJogos=1,

                        Classificacao_CampeonatoId = partida.FK_Campeonato_Id
                    };

                    var classificacaoFora = new ClassificacaoModel
                    {
                        Classificacao_PartidaId = partida.Id,
                        Classificacao_Campo = partida.Partida_NumeroCampo,
                        Classificacao_TimeId = partida.TimeForaId,
                        Time = timeRepository.GetById(partida.TimeForaId),
                        Classificacao_Vitoria = partida.IsTimeForaVencedor ? 1 : 0,
                        Classificacao_Derrota = partida.IsTimeCasaVencedor ? 1 : 0,
                        Classificacao_PontosPro = partida.Partida_PontosFora,
                        Classificacao_PontosContra = partida.Partida_PontosCasa,
                        Classificacao_QtdeJogos = 1,
                        Classificacao_CampeonatoId = partida.FK_Campeonato_Id
                    };

                    classificacaoRepository.InsertOrReplace(classificacaoCasa);
                    classificacaoRepository.InsertOrReplace(classificacaoFora);

                    partidaRepository.InsertOrReplace(partida);
                }
            }
           // Teste();
        }

        private void Teste()
        {
            var classificacaoRepository = new ClassificacaoRepository();
            var timeRepository = new TimeRepository();
            var campeonatoRepository = new CampeonatoRepository();
            var Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            var ListaClassificacaoCampo1 = classificacaoRepository.GetAll()
                .Where(x =>
                    x.Classificacao_CampeonatoId == Campeonato.Id &&
                    x.Classificacao_Campo == 1)
                .GroupBy(x => x.Classificacao_TimeId)
                .Select((g, index) => new ClassificacaoModel
                {
                    Classificacao_TimeId = g.Key,
                    Classificacao_CampeonatoId = Campeonato.Id,
                    Classificacao_Vitoria = g.Sum(x => x.Classificacao_Vitoria),
                    Classificacao_Derrota = g.Sum(x => x.Classificacao_Derrota),
                    Classificacao_PontosPro = g.Sum(x => x.Classificacao_PontosPro),
                    Classificacao_PontosContra = g.Sum(x => x.Classificacao_PontosContra),

                    // 🔧 Usar Count() em vez de somar campo problemático
                    Classificacao_QtdeJogos = g.Count(),
                    Classificacao_PartidaId = g.FirstOrDefault().Classificacao_PartidaId,
                    Classificacao_Campo = 1,
                    Time = timeRepository.GetById(g.Key),
                    IsEven = index % 2 == 0
                })
                .OrderByDescending(x => x.Classificacao_Vitoria)
                .ThenByDescending(x => x.Classificacao_SaldoPontos)
                .ThenByDescending(x => x.Classificacao_PontosPro)
                .Select((x, i) =>
                {
                    x.Classificacao_Posicao = i + 1;
                    return x;
                })
                .ToList();
        }
    }
}