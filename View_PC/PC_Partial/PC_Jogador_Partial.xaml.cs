using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC.PC_Partial;

public partial class PC_Jogador_Partial : ContentView
{
    public PC_Jogador_Partial(PC_DashBoardViewModel pc_DashBoardVM)
    {
        InitializeComponent();
        BindingContext = new PC_Jogador_PartialViewModel(pc_DashBoardVM);
    }
}