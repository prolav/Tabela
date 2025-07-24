namespace Tabela;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("TopoTemplate", typeof(Views_Celular.Templates_Celular.TopoTemplate));
        Routing.RegisterRoute("PC_LoginView", typeof(View_PC.PC_LoginView));
    }
}