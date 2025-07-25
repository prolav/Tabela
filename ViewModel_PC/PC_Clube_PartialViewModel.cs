using System.Windows.Input;
using Tabela.Models;

namespace Tabela.ViewModel_PC;

public class PC_Clube_PartialViewModel : BaseViewModel
{
    #region Fields
     private List<ClubeModel> _listaClube;
     private PC_DashBoardViewModel _pc_DashBoardVM;
     #endregion
 
     #region Properties
     public List<ClubeModel> ListaClube { get => _listaClube; set => SetProperty(ref _listaClube, value); }
     //
     // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
     #endregion
         
     #region Commands
     public ICommand CadastrarClubeCommand => new Command(() => CadastrarClubeExecute());
     #endregion
         
     #region Constructor
     public PC_Clube_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
     {
         ListaClube = new List<ClubeModel>();
         _pc_DashBoardVM = pc_DashBoardVM;
     }
     #endregion
 
     #region Methods
     private void CadastrarClubeExecute()
     {
         _pc_DashBoardVM.MudancaPage(PC_DashBoardViewModel.TipoPage.CadastroClube);
 
     }
     #endregion
 }
    