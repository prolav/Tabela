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
    public ICommand EntrarCommand => new Command(() => EntrarCommandExecute());
    public ICommand DroparBancoCommand => new Command(() => DroparBancoExecute());
    #endregion
    
    #region Constructor
    public PC_LoginViewModel()
    {

    }
    #endregion

    #region Methods
    private void EntrarCommandExecute()
    {
        Application.Current.MainPage = new NavigationPage(new PC_DashBoardView());

    }

    private async void DroparBancoExecute()
    {
        bool resposta = await Application.Current.MainPage.DisplayAlert("Atenção", "Deletar Banco?", "Sim", "Não");
        if (resposta)
        {
            try
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "GateScore.db3");

                if (File.Exists(dbPath))
                    File.Delete(dbPath); // 🔥 Deleta o banco inteiro
                else
                {
                    Console.WriteLine();  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    #endregion
}