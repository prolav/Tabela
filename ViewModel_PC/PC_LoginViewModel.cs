using System.Windows.Input;
using Tabela.View_PC;

namespace Tabela.ViewModel_PC;

public class PC_LoginViewModel : BaseViewModel
{
    #region Fields
    private string _nomeUsuario;
    private string _senha;


    #endregion

    #region Properties
    public string NomeUsuario { get => _nomeUsuario; set => SetProperty(ref _nomeUsuario, value); }

    public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand EntrarCommand { get; }
    #endregion
    
    #region Constructor
    public PC_LoginViewModel()
    {
        EntrarCommand = new Command(EntrarCommandExecute);
    }
    #endregion

    #region Methods
    private void EntrarCommandExecute()
    {
        Application.Current.MainPage = new NavigationPage(new PC_DashBoardView());

    }
    #endregion
}