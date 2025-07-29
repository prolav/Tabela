using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Tabela.ViewModel_PC;

public class PC_CadastroGeral_PartialViewModel: BaseViewModel
{
    #region Fields
    // private string _nomeUsuario;
    private PC_DashBoardViewModel _pc_DashBoardVM;

    // private string _imagemJogador;
    #endregion

    #region Properties
    // public string NomeUsuario { get => _nomeUsuario; set => SetProperty(ref _nomeUsuario, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand AtualizarPageCommand => new Command<string>(
        nomePage => _pc_DashBoardVM.AtualizarPage(nomePage)
    );

    #endregion
    
    #region Constructor
    public PC_CadastroGeral_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        //ImagemClube = ImageSource.FromFile("sem_imagem.jpeg");
    }
    #endregion

    #region Methods

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}
