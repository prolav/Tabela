using System.Windows.Input;

namespace Tabela.ViewModel_PC;

public class PC_DashBoard_PartialViewModel : BaseViewModel
{
    
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;

    private ImageSource _imagemClube;
    // private string _senha;
    #endregion

    #region Properties
    public ImageSource ImagemClube { get => _imagemClube; set => SetProperty(ref _imagemClube, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemClubeExecute());
    public ICommand AdicionarClubeCommand => new Command(() => AdicionarClubeExecute());
    #endregion
    
    #region Constructor
    public PC_DashBoard_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;

    }
    #endregion

    #region Methods
    private void AdicionarImagemClubeExecute()
    {
        

    }

    private void AdicionarClubeExecute()
    {


    }
    #endregion
}