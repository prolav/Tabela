using Tabela.View_PC;

namespace Tabela;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new PC_LoginView();
    }
}