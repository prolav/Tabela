using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_DashBoard_PartialViewModel : BaseViewModel
{
    
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<PartidaModel> _listaPartida;
    private CampeonatoModel _campeonato;
    private int _qtdePartidas;
    #endregion

    #region Properties
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    public List<PartidaModel> ListaPartida { get => _listaPartida; set => SetProperty(ref _listaPartida, value); }
    public int QtdePartidas { get => _qtdePartidas; set => SetProperty(ref _qtdePartidas, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemClubeExecute());
    public ICommand AdicionarClubeCommand => new Command(() => AdicionarClubeExecute());
    #endregion
    
    #region Constructor
    public PC_DashBoard_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
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
            Campeonato = new CampeonatoModel();
            Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            var faseRepository = new FasesRepository();
            var partidaRepository = new PartidaRepository();
            ListaPartida = new List<PartidaModel>();
            ListaPartida= partidaRepository.GetAll().Where(a => a.FK_Campeonato_Id == Campeonato.Id).ToList();
            QtdePartidas = ListaPartida.Count;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void AdicionarImagemClubeExecute()
    {
        

    }

    private void AdicionarClubeExecute()
    {


    }
    #endregion
}