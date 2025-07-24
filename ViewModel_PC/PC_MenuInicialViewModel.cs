using System.Windows.Input;
using Tabela.View_PC;

namespace Tabela.ViewModel_PC;

public class PC_MenuInicialViewModel : BaseViewModel
{
    #region Fields
    // private string _nomeUsuario;
    // private string _senha;
    #endregion

    #region Properties
    // public string NomeUsuario { get => _nomeUsuario; set => SetProperty(ref _nomeUsuario, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
    
    #region Commands
    public ICommand SairCommand { get; }
    #endregion
    
    #region Constructor
    public PC_MenuInicialViewModel()
    {
        SairCommand = new Command(SairCommandExecute);
    }
    #endregion

    #region Methods
    private void SairCommandExecute()
    {
        

    }
    #endregion
}