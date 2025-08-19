using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabela.Models;
using Tabela.ViewModel_PC;

namespace Tabela.View_PC.PC_Partial;

public partial class PC_CadastroJogador_Partial : ContentView
{
    public PC_CadastroJogador_Partial(PC_DashBoardViewModel pc_DashBoardVM, JogadorModel jogador, bool modoEdicao)
    {
        InitializeComponent();
        BindingContext = new PC_CadastroJogador_PartialViewModel(pc_DashBoardVM, jogador, modoEdicao);
    }
}