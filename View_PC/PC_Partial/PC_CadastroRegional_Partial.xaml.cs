using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.Models;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC.PC_Partial;

public partial class PC_CadastroRegional_Partial : ContentView
{
    public PC_CadastroRegional_Partial(PC_DashBoardViewModel pc_DashBoardVM, RegionalModel regional = null, bool modoEdicao = false)
    {
        InitializeComponent();
        BindingContext = new PC_CadastroRegional_PartialViewModel(pc_DashBoardVM, regional, modoEdicao);
    }
}