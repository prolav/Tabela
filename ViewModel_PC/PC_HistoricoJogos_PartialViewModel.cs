using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_HistoricoJogos_PartialViewModel: BaseViewModel
{
    
    #region Fields
    // private string _nomeUsuario;
    private List<PartidaModel> _listaPartidaCampo1;
    private List<PartidaModel> _listaPartidaCampo2;
    private List<PartidaModel> _listaPartidaCampo3;
    private List<PartidaModel> _listaPartidaCampo4;
    private List<PartidaModel> _listaPartidaCampo5;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private CampeonatoModel _campeonato;
    #endregion

    #region Properties
    public List<PartidaModel> ListaPartidasCampo1 { get => _listaPartidaCampo1; set => SetProperty(ref _listaPartidaCampo1, value); }
    public List<PartidaModel> ListaPartidasCampo2 { get => _listaPartidaCampo2; set => SetProperty(ref _listaPartidaCampo2, value); }
    public List<PartidaModel> ListaPartidasCampo3 { get => _listaPartidaCampo3; set => SetProperty(ref _listaPartidaCampo3, value); }
    public List<PartidaModel> ListaPartidasCampo4 { get => _listaPartidaCampo4; set => SetProperty(ref _listaPartidaCampo4, value); }
    public List<PartidaModel> ListaPartidasCampo5 { get => _listaPartidaCampo5; set => SetProperty(ref _listaPartidaCampo5, value); }
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemClubeExecute());
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
            ListaPartidasCampo1 = new List<PartidaModel>();
            ListaPartidasCampo2 = new List<PartidaModel>();
            ListaPartidasCampo3 = new List<PartidaModel>();
            ListaPartidasCampo4 = new List<PartidaModel>();
            ListaPartidasCampo5 = new List<PartidaModel>();
            CarregarCamposPartidas();
            

        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void CarregarObjetosNulos(List<PartidaModel> listaPartida)
    {
        try
        {
            var montagemTimeRepository = new MontagemCampeonatoRepository();
            foreach (var partida in listaPartida)
            {
                partida.TimeCasa = montagemTimeRepository.GetById(partida.TimeCasaId);
                partida.TimeFora =  montagemTimeRepository.GetById(partida.TimeForaId);
                partida.TimeJuiz =  montagemTimeRepository.GetById(partida.TimeJuizId);
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
            var partidaRepository = new PartidaRepository();
            var listaPartidas = new List<PartidaModel>();
            listaPartidas = partidaRepository.GetAll().Where(a => a.FK_Campeonato_Id == Campeonato.Id).ToList();
            CarregaCorLista(listaPartidas);
            CarregarObjetosNulos(listaPartidas);
            if (Campeonato.Campeonato_NumerosCampos >= 1)
                ListaPartidasCampo1 = listaPartidas.Where(a => a.Partida_NumeroCampo == 1).ToList(); 
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                ListaPartidasCampo2 = listaPartidas.Where(a => a.Partida_NumeroCampo == 2).ToList(); 
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                ListaPartidasCampo3 = listaPartidas.Where(a => a.Partida_NumeroCampo == 3).ToList(); 
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                ListaPartidasCampo4 = listaPartidas.Where(a => a.Partida_NumeroCampo == 4).ToList(); 
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                ListaPartidasCampo5 = listaPartidas.Where(a => a.Partida_NumeroCampo == 5).ToList(); 

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregaCorLista(List<PartidaModel> listaPartida)
    {
        try
        {
            for (int i = 0; i < listaPartida.Count; i++)
                listaPartida[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void AdicionarImagemClubeExecute()
    {

    }
    #endregion

}