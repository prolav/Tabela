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
            foreach (var partida in listaPartida)
            {
                if (partida.CadastradoClassificacao == false)
                {               
                    var classificacaoCasa = GetAll().FirstOrDefault(c => c.Classificacao_TimeId == partida.TimeCasaId);
                    if (classificacaoCasa == null)
                        classificacaoCasa = new ClassificacaoModel();
                    classificacaoCasa.Classificacao_Campo = partida.Partida_NumeroCampo;
                    classificacaoCasa.Classificacao_TimeId = partida.TimeCasaId;
                    classificacaoCasa.Classificacao_Vitoria += partida.IsTimeCasaVencedor ? 1 : 0;
                    classificacaoCasa.Classificacao_Derrota += partida.IsTimeForaVencedor ? 1 : 0;
                    classificacaoCasa.Classificacao_PontosPro += partida.Partida_PontosCasa;
                    classificacaoCasa.Classificacao_PontosContra += partida.Partida_PontosFora;
                    classificacaoCasa.Classificacao_CampeonatoId = partida.FK_Campeonato_Id;

                    var classificacaoFora = GetAll().FirstOrDefault(c => c.Classificacao_TimeId == partida.TimeCasaId);
                    if (classificacaoFora == null)
                        classificacaoFora = new ClassificacaoModel();
                    classificacaoFora.Classificacao_Campo = partida.Partida_NumeroCampo;
                    classificacaoFora.Classificacao_TimeId = partida.TimeCasaId;
                    classificacaoFora.Classificacao_Vitoria += partida.IsTimeCasaVencedor ? 1 : 0;
                    classificacaoFora.Classificacao_Derrota += partida.IsTimeForaVencedor ? 1 : 0;
                    classificacaoFora.Classificacao_PontosPro += partida.Partida_PontosCasa;
                    classificacaoFora.Classificacao_PontosContra += partida.Partida_PontosFora;
                    classificacaoFora.Classificacao_CampeonatoId = partida.FK_Campeonato_Id;

                    var classificacaoRepository = new ClassificacaoRepository();
                    var partidaRepository = new PartidaRepository();
                    partida.CadastradoClassificacao = true;
                    classificacaoRepository.InsertOrReplace(classificacaoCasa);
                    classificacaoRepository.InsertOrReplace(classificacaoFora);
                    partidaRepository.InsertOrReplace(partida);
                }
            }      
        }
    }
}