using System.Windows.Input;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroClube_PartialViewModel: BaseViewModel
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
    public PC_CadastroClube_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        ImagemClube = ImageSource.FromResource("sem_imagem.jpeg");
    }
    #endregion

    #region Methods
    private void AdicionarImagemClubeExecute()
    {
        

    }

    private void AdicionarClubeExecute()
    {
       // var listaClubes = new ClubeRepository();
        //var match = listaClubes.; 

    }
    #endregion
}