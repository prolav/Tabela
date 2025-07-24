using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC;

public partial class PC_DashBoardView : ContentPage
{
    public PC_DashBoardView()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new PC_DashBoardViewModel();
    }
}