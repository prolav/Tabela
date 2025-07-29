using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.Models;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC.PC_Partial;

public partial class PC_CadastroFase_Partial : ContentView
{
    public PC_CadastroFase_Partial(PC_DashBoardViewModel pc_DashBoardVM, FaseModel fase = null)
    {
        InitializeComponent();
        BindingContext = new PC_CadastroFase_PartialViewModel(pc_DashBoardVM, fase);
    }
}