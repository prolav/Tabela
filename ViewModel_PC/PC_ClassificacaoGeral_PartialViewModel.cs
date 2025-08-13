using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_ClassificacaoGeral_PartialViewModel:BaseViewModel
{
    
    #region Fields
    // private string _nomeUsuario;
    private CampeonatoModel _campeonato;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    #endregion

    #region Properties
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemClubeExecute());
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
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }
    private void AdicionarImagemClubeExecute()
    {

    }
    #endregion
}