using System.Windows.Input;

namespace Tabela.ViewModel_PC;

public class PC_CadastroJogador_PartialViewModel : BaseViewModel
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;

    private string _imagemJogador;
    // private string _senha;
    #endregion

    #region Properties
    public string ImagemClube { get => _imagemJogador; set => SetProperty(ref _imagemJogador, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemJogadorExecute());
    public ICommand AdicionarClubeCommand => new Command(() => AdicionarJogadorExecute());
    #endregion
    
    #region Constructor
    public PC_CadastroJogador_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        ImagemClube = "sem_imagem.jpeg";
    }
    #endregion

    #region Methods
    private void AdicionarImagemJogadorExecute()
    {
        

    }

    private void AdicionarJogadorExecute()
    {
        // var listaClubes = new ClubeRepository();
        //var match = listaClubes.; 

    }
    #endregion
}