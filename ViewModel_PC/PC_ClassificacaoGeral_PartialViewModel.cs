using System.Windows.Input;

namespace Tabela.ViewModel_PC;

public class PC_ClassificacaoGeral_PartialViewModel:BaseViewModel
{
    
    #region Fields
    // private string _nomeUsuario;
    // private string _senha;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    #endregion

    #region Properties
    // public string NomeUsuario { get => _nomeUsuario; set => SetProperty(ref _nomeUsuario, value); }
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
    }
    #endregion

    #region Methods

    private void AdicionarImagemClubeExecute()
    {

    }
    #endregion
}