using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC;

public partial class PC_CadastroGeralView : ContentPage
{
    public PC_CadastroGeralView(PC_DashBoardViewModel pc_DashBoardVM)
    {
        InitializeComponent();
        BindingContext = new PC_CadastroGeralViewModel();
    }
}