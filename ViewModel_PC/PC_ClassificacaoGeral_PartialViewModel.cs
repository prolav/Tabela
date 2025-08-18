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
            var listaClassificacaoCampos = new List<ClassificacaoModel>();
            listaClassificacaoCampos = classificacaoRepository.GetAll()
                        .Where(x => x.Classificacao_CampeonatoId == Campeonato.Id && x.Classificacao_Campo == campo)
                        .GroupBy(x => new { x.Classificacao_TimeId, x.Classificacao_Campo })
                        .Select((g, index) => new ClassificacaoModel
                        {
                            Classificacao_TimeId = g.Key.Classificacao_TimeId,
                            Classificacao_CampeonatoId = Campeonato.Id,
                            Classificacao_Vitoria = g.Sum(x => x.Classificacao_Vitoria),
                            Classificacao_Derrota = g.Sum(x => x.Classificacao_Derrota),
                            Classificacao_PontosPro = g.Sum(x => x.Classificacao_PontosPro),
                            Classificacao_PontosContra = g.Sum(x => x.Classificacao_PontosContra),
                            Classificacao_QtdeJogos = g.Sum(x => x.Classificacao_Vitoria + x.Classificacao_Derrota),
                            Classificacao_Campo = g.Key.Classificacao_Campo, // ✅ pegando da lista
                            Time = timeRepository.GetById(g.Key.Classificacao_TimeId),
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
            ListaClassificacaoCampo =  listaClassificacaoCampos;
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void GerarClassificacaoGeral()
    {
        try
        {
            var classificacaoRepository = new ClassificacaoRepository();
            var timeRepository = new TimeRepository();
            var classificacoesGerais = classificacaoRepository.GetAll()
                .Where(x => x.Classificacao_CampeonatoId == Campeonato.Id)
                .GroupBy(x => x.Classificacao_TimeId)
                .Select((g, index) => new ClassificacaoModel
                {
                    Classificacao_TimeId = g.Key,
                    Classificacao_CampeonatoId = Campeonato.Id,
                    Classificacao_Vitoria = g.Sum(x => x.Classificacao_Vitoria),
                    Classificacao_Derrota = g.Sum(x => x.Classificacao_Derrota),
                    Classificacao_PontosPro = g.Sum(x => x.Classificacao_PontosPro),
                    Classificacao_PontosContra = g.Sum(x => x.Classificacao_PontosContra),
                    Classificacao_QtdeJogos = g.Sum(x => x.Classificacao_QtdeJogos),
                    Classificacao_Campo = 0, // Opcional: pode usar 0 ou -1 para indicar "Geral"
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
            ListaClassificacaoCampo =  classificacoesGerais;
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }
    #endregion
}