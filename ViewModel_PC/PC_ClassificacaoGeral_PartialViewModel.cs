using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;
using Tabela.Repositories.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_ClassificacaoGeral_PartialViewModel:BaseViewModel
{
    
    #region Fields
    private CampeonatoModel _campeonato;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<ClassificacaoModel> _listaClassificacaoCampo;
    private bool _buttonCampo2_IsVisible;
    private bool _buttonCampo3_IsVisible;
    private bool _buttonCampo4_IsVisible;
    private bool _buttonCampo5_IsVisible;
    #endregion

    #region Properties
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo { get => _listaClassificacaoCampo; set => SetProperty(ref _listaClassificacaoCampo, value); }
    public bool ButtonCampo2_IsVisible { get => _buttonCampo2_IsVisible; set => SetProperty(ref _buttonCampo2_IsVisible, value); }
    public bool ButtonCampo3_IsVisible { get => _buttonCampo3_IsVisible; set => SetProperty(ref _buttonCampo3_IsVisible, value); }
    public bool ButtonCampo4_IsVisible { get => _buttonCampo4_IsVisible; set => SetProperty(ref _buttonCampo4_IsVisible, value); }
    public bool ButtonCampo5_IsVisible { get => _buttonCampo5_IsVisible; set => SetProperty(ref _buttonCampo5_IsVisible, value); }
    #endregion

    #region Commands
    public ICommand AtualizarListaCommand => new Command<object>(param =>
    {
        var numeroCampo = Convert.ToInt32(param); 
        if (numeroCampo >= 1 && numeroCampo <= 6)
        {
            GerarListaCampos(numeroCampo);
        }
        else
        {
            GerarClassificacaoGeral();
        }
    });
    #endregion
    
    #region Constructor
    public PC_ClassificacaoGeral_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados();
    }
    #endregion

    #region Methods

    private void CarregarDados()
    {
        try
        {
            var campeonatoRepository = new CampeonatoRepository();
            Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            GerarListaCampos(1);
            CarregarButtons();
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void CarregarButtons()
    {
        try
        {
            var maxCampos  = Campeonato.Campeonato_NumerosCampos;
            if (maxCampos == 5)
            {
                ButtonCampo2_IsVisible = true;
                ButtonCampo3_IsVisible = true;
                ButtonCampo4_IsVisible = true;
                ButtonCampo5_IsVisible = true;
            }
            else if (maxCampos == 4)
            {
                ButtonCampo2_IsVisible = true;
                ButtonCampo3_IsVisible = true;
                ButtonCampo4_IsVisible = true;
            }
            else if (maxCampos == 3)
            {
                ButtonCampo2_IsVisible = true;
                ButtonCampo3_IsVisible = true;
            }
            else if (maxCampos == 2)
            {
                ButtonCampo2_IsVisible = true;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void GerarListaCampos(int campo)
    {
        try
        {
            var classificacaoRepository = new ClassificacaoRepository();
            var timeRepository = new TimeRepository();

            var classificacoesCampo = classificacaoRepository.GetAll()
                .Where(x => x.Classificacao_CampeonatoId == Campeonato.Id && x.Classificacao_Campo == campo)
                .ToList();

            // Agrupa por time
            var listaClassificacaoCampos = classificacoesCampo
                .GroupBy(x => x.Classificacao_TimeId)
                .Select(g =>
                {
                    int vitorias = g.Sum(x => x.Classificacao_Vitoria);
                    int derrotas = g.Sum(x => x.Classificacao_Derrota);
                    int pontosPro = g.Sum(x => x.Classificacao_PontosPro);
                    int pontosContra = g.Sum(x => x.Classificacao_PontosContra);

                    return new ClassificacaoModel
                    {
                        Classificacao_TimeId = g.Key,
                        Classificacao_CampeonatoId = Campeonato.Id,
                        Classificacao_Vitoria = vitorias,
                        Classificacao_Derrota = derrotas,
                        Classificacao_PontosPro = pontosPro,
                        Classificacao_PontosContra = pontosContra,
                        Classificacao_QtdeJogos = vitorias + derrotas,
                        Classificacao_Campo = campo,
                        Time = timeRepository.GetById(g.Key)
                    };
                })
                // Ordena ranking: vitórias > saldo > pontos pro
                .OrderByDescending(x => x.Classificacao_Vitoria)
                .ThenByDescending(x => x.Classificacao_SaldoPontos)
                .ThenByDescending(x => x.Classificacao_PontosPro)
                .ToList();

            // Calcula posições considerando empates
            int posicao = 1;
            for (int i = 0; i < listaClassificacaoCampos.Count; i++)
            {
                if (i > 0)
                {
                    var anterior = listaClassificacaoCampos[i - 1];
                    var atual = listaClassificacaoCampos[i];

                    // Se empate total, mantém mesma posição
                    if (atual.Classificacao_Vitoria == anterior.Classificacao_Vitoria &&
                        atual.Classificacao_SaldoPontos == anterior.Classificacao_SaldoPontos &&
                        atual.Classificacao_PontosPro == anterior.Classificacao_PontosPro)
                    {
                        atual.Classificacao_Posicao = anterior.Classificacao_Posicao;
                    }
                    else
                    {
                        atual.Classificacao_Posicao = i + 1;
                    }
                }
                else
                {
                    listaClassificacaoCampos[i].Classificacao_Posicao = posicao;
                }

                listaClassificacaoCampos[i].IsEven = (i % 2 == 0);
            }

            ListaClassificacaoCampo = listaClassificacaoCampos;
            OnPropertyChanged(nameof(ListaClassificacaoCampo));
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao gerar lista de classificação por campo: {e.Message}");
        }
    }

    private void GerarClassificacaoGeral()
    {
        try
        {
            var classificacaoRepository = new ClassificacaoRepository();
            var timeRepository = new TimeRepository();

            // Busca todas as classificações do campeonato
            var classificacoes = classificacaoRepository.GetAll()
                .Where(x => x.Classificacao_CampeonatoId == Campeonato.Id)
                .ToList();

            // Agrupa por time
            var listaClassificacaoGeral = classificacoes
                .GroupBy(x => x.Classificacao_TimeId)
                .Select(g =>
                {
                    int vitorias = g.Sum(x => x.Classificacao_Vitoria);
                    int derrotas = g.Sum(x => x.Classificacao_Derrota);
                    int pontosPro = g.Sum(x => x.Classificacao_PontosPro);
                    int pontosContra = g.Sum(x => x.Classificacao_PontosContra);

                    return new ClassificacaoModel
                    {
                        Classificacao_TimeId = g.Key,
                        Classificacao_CampeonatoId = Campeonato.Id,
                        Classificacao_Vitoria = vitorias,
                        Classificacao_Derrota = derrotas,
                        Classificacao_PontosPro = pontosPro,
                        Classificacao_PontosContra = pontosContra,
                        Classificacao_QtdeJogos = vitorias + derrotas,
                        Classificacao_Campo = 0, // Geral
                        Time = timeRepository.GetById(g.Key)
                    };
                })
                // Ordena ranking: vitórias > saldo > pontos pró
                .OrderByDescending(x => x.Classificacao_Vitoria)
                .ThenByDescending(x => x.Classificacao_SaldoPontos)
                .ThenByDescending(x => x.Classificacao_PontosPro)
                .ToList();

            // Calcula posições considerando empates
            for (int i = 0; i < listaClassificacaoGeral.Count; i++)
            {
                if (i > 0)
                {
                    var anterior = listaClassificacaoGeral[i - 1];
                    var atual = listaClassificacaoGeral[i];

                    if (atual.Classificacao_Vitoria == anterior.Classificacao_Vitoria &&
                        atual.Classificacao_SaldoPontos == anterior.Classificacao_SaldoPontos &&
                        atual.Classificacao_PontosPro == anterior.Classificacao_PontosPro)
                    {
                        atual.Classificacao_Posicao = anterior.Classificacao_Posicao;
                    }
                    else
                    {
                        atual.Classificacao_Posicao = i + 1;
                    }
                }
                else
                {
                    listaClassificacaoGeral[i].Classificacao_Posicao = 1;
                }

                listaClassificacaoGeral[i].IsEven = (i % 2 == 0);
            }

            ListaClassificacaoCampo = listaClassificacaoGeral;
            OnPropertyChanged(nameof(ListaClassificacaoCampo));
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    #endregion
}