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
                // 1️⃣ Salva ou atualiza a partida
                partidaRepository.InsertOrReplace(partida);

                // --- TIME CASA ---
                var classificacaoCasa = classificacaoRepository.GetAll()
                    .FirstOrDefault(c => c.Classificacao_PartidaId == partida.Id &&
                                         c.Classificacao_TimeId == partida.TimeCasaId);

                if (classificacaoCasa == null)
                {
                    classificacaoCasa = new ClassificacaoModel
                    {
                        Classificacao_PartidaId = partida.Id,
                        Classificacao_TimeId = partida.TimeCasaId,
                        Classificacao_CampeonatoId = partida.FK_Campeonato_Id,
                        Time = timeRepository.GetById(partida.TimeCasaId)
                    };
                }

                classificacaoCasa.Classificacao_Campo = partida.Partida_NumeroCampo;
                classificacaoCasa.Classificacao_Vitoria = partida.IsTimeCasaVencedor ? 1 : 0;
                classificacaoCasa.Classificacao_Derrota = partida.IsTimeForaVencedor ? 1 : 0;
                classificacaoCasa.Classificacao_PontosPro = partida.Partida_PontosCasa;
                classificacaoCasa.Classificacao_PontosContra = partida.Partida_PontosFora;
                classificacaoCasa.Classificacao_QtdeJogos = classificacaoCasa.Classificacao_Vitoria + classificacaoCasa.Classificacao_Derrota;

                classificacaoRepository.InsertOrReplace(classificacaoCasa);

                // --- TIME FORA ---
                var classificacaoFora = classificacaoRepository.GetAll()
                    .FirstOrDefault(c => c.Classificacao_PartidaId == partida.Id &&
                                         c.Classificacao_TimeId == partida.TimeForaId);

                if (classificacaoFora == null)
                {
                    classificacaoFora = new ClassificacaoModel
                    {
                        Classificacao_PartidaId = partida.Id,
                        Classificacao_TimeId = partida.TimeForaId,
                        Classificacao_CampeonatoId = partida.FK_Campeonato_Id,
                        Time = timeRepository.GetById(partida.TimeForaId)
                    };
                }

                classificacaoFora.Classificacao_Campo = partida.Partida_NumeroCampo;
                classificacaoFora.Classificacao_Vitoria = partida.IsTimeForaVencedor ? 1 : 0;
                classificacaoFora.Classificacao_Derrota = partida.IsTimeCasaVencedor ? 1 : 0;
                classificacaoFora.Classificacao_PontosPro = partida.Partida_PontosFora;
                classificacaoFora.Classificacao_PontosContra = partida.Partida_PontosCasa;
                classificacaoFora.Classificacao_QtdeJogos = classificacaoFora.Classificacao_Vitoria + classificacaoFora.Classificacao_Derrota;

                classificacaoRepository.InsertOrReplace(classificacaoFora);
            }
        }


    }
}