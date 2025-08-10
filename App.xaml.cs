using Tabela.View_PC;

namespace Tabela;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();


        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            Application.Current.MainPage.DisplayAlert("Atenção", $"Está funcionando somente em PC de 24 pol", "OK");
        }
        else if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            MainPage = new PC_LoginView();
        }
    }
}