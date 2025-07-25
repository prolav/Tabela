using System.Windows.Input;
using Tabela.Models;

namespace Tabela.ViewModel_PC;

public class PC_NovoCampeonato_PartialViewModel:BaseViewModel
{
    
    #region Fields
    private List<JogadorModel> _listaJogador;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    #endregion

    #region Properties
    public List<JogadorModel> ListaClube { get => _listaJogador; set => SetProperty(ref _listaJogador, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
            
    #region Commands
    public ICommand CadastrarClubeCommand => new Command(() => CadastrarJogadorExecute());
    #endregion
            
    #region Constructor
    public PC_NovoCampeonato_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        ListaClube = new List<JogadorModel>();
        _pc_DashBoardVM = pc_DashBoardVM;
    }
    #endregion

    #region Methods
    private void CadastrarJogadorExecute()
    {
        _pc_DashBoardVM.MudancaPage(PC_DashBoardViewModel.TipoPage.CadastroJogador);

    }
    #endregion
}