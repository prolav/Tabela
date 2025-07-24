using Tabela.ViewModel_PC;

namespace Tabela.View_PC;

public partial class PC_LoginView : ContentPage
{
    public PC_LoginView()
    {
        InitializeComponent();
        BindingContext = new PC_LoginViewModel();
    }
}