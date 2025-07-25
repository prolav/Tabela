using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC.PC_Partial;

public partial class PC_CadastroClube_Partial : ContentView
{
    public PC_CadastroClube_Partial(PC_DashBoardViewModel pc_DashBoardVM)
    {
        InitializeComponent();
        BindingContext = new PC_CadastroClube_PartialViewModel(pc_DashBoardVM);
    }
}