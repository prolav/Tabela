using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;
using Tabela.Repositories.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_HistoricoJogos_PartialViewModel: BaseViewModel
{
    
    #region Fields
    // private string _nomeUsuario;
    private List<PartidaModel> _listaPartidas;

    private PC_DashBoardViewModel _pc_DashBoardVM;
    private CampeonatoModel _campeonato;
    private bool _buttonCampo2_IsVisible;
    private bool _buttonCampo3_IsVisible;
    private bool _buttonCampo4_IsVisible;
    private bool _buttonCampo5_IsVisible;
    private PartidaRepository _partidaRepository;
    #endregion

    #region Properties
    public List<PartidaModel> ListaPartidas { get => _listaPartidas; set => SetProperty(ref _listaPartidas, value); }
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    public bool ButtonCampo2_IsVisible { get => _buttonCampo2_IsVisible; set => SetProperty(ref _buttonCampo2_IsVisible, value); }
    public bool ButtonCampo3_IsVisible { get => _buttonCampo3_IsVisible; set => SetProperty(ref _buttonCampo3_IsVisible, value); }
    public bool ButtonCampo4_IsVisible { get => _buttonCampo4_IsVisible; set => SetProperty(ref _buttonCampo4_IsVisible, value); }
    public bool ButtonCampo5_IsVisible { get => _buttonCampo5_IsVisible; set => SetProperty(ref _buttonCampo5_IsVisible, value); }
    #endregion
    
    #region Commands
    public ICommand SalvarResultadosCommand => new Command(() => SalvarResultadosExecute(ListaPartidas[0].Partida_NumeroCampo));
    public ICommand MostrarCampoCommand => new Command<string>(
        obj => MostrarCampoExecute(obj));
    #endregion
    
    #region Constructor
    public PC_HistoricoJogos_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
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
            _partidaRepository = new PartidaRepository();
            ListaPartidas = new List<PartidaModel>();
            ListaPartidas = _partidaRepository.GetAll().Where(a => a.FK_Campeonato_Id == Campeonato.Id && a.Partida_NumeroCampo == 1).OrderBy(a =>a.Partida_Rodada).ToList();
            CarregarButtonsIsVisible();
            CarregarCamposPartidas();
            CarregarClassificacaoGeral();
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void CarregarClassificacaoGeral()
    {
        try
        {
            for (int i = 1; i <= Campeonato.Campeonato_NumerosCampos; i++)
            {
                SalvarResultadosExecute(i);
            }
            var campeonatoRepository = new CampeonatoRepository();
            Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            Campeonato.Campeonato_HistoricoJogos_Cadastrado = true;
            campeonatoRepository.Insert(Campeonato);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private void CarregarObjetosNulos()
    {
        try
        {
            var timeRepository = new TimeRepository();
            foreach (var partida in ListaPartidas)
            {
                partida.TimeCasa = timeRepository.GetById(partida.TimeCasaId);
                partida.TimeFora =  timeRepository.GetById(partida.TimeForaId);
                partida.TimeJuiz =  timeRepository.GetById(partida.TimeJuizId);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void CarregarCamposPartidas()
    {
        try
        {
            CarregaCorLista();
            CarregarObjetosNulos();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarButtonsIsVisible()
    {
        try
        {
            var maxCampos  = _partidaRepository.GetAll().Where(a => a.FK_Campeonato_Id == Campeonato.Id)
                .Max(i => i.Partida_NumeroCampo);
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
    private void CarregaCorLista()
    {
        try
        {
            for (int i = 0; i < ListaPartidas.Count; i++)
                ListaPartidas[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void MostrarCampoExecute(string obj)
    {
        try
        {
            var campo = int.Parse(obj);
            SalvarResultadosExecute(campo);
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void SalvarResultadosExecute(int campo)
    {
        try
        {
            foreach (var partida in ListaPartidas)
            {
                _partidaRepository.InsertOrReplace(partida);              
            }
            var classificacaoRepository = new ClassificacaoRepository();
            classificacaoRepository.SalvarClassificacaoByPartida(ListaPartidas);
            ListaPartidas = _partidaRepository.GetAll().Where(a => a.FK_Campeonato_Id == Campeonato.Id && a.Partida_NumeroCampo == campo).ToList();
            CarregarCamposPartidas();
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }
    #endregion

}