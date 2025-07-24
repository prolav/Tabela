using Tabela.ViewModels_Celular;

namespace Tabela.Views_Celular.Templates_Celular;

public partial class TopoTemplate : ContentView
{
    public TopoTemplate()
    {
        InitializeComponent();
        BindingContext = new TopoTemplateViewModel();
    }

    private void OnCardClicked(object sender, EventArgs e)
    {

    }
}