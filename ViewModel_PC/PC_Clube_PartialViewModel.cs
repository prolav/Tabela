using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_Clube_PartialViewModel : BaseViewModel
{
    #region Fields
     private PC_DashBoardViewModel _pc_DashBoardVM;
     private List<ClubeModel>   _listaClube;
     private ClubeModel _clubeSelecionado;
     #endregion
 
     #region Properties
     public List<ClubeModel> ListaClube { get => _listaClube; set => SetProperty(ref _listaClube, value); }
     public ClubeModel ClubeSelecionado
     {
         get => _clubeSelecionado;
         set
         {
             if (_clubeSelecionado != value)
             {
                 _clubeSelecionado = value;
                 OnPropertyChanged();

                 if (_clubeSelecionado != null)
                     EditarClubeExecute(_clubeSelecionado);
             }
         }
     }

     #endregion
         
     #region Commands
     public ICommand CadastrarClubeCommand => new Command(() => CadastrarClubeExecute());
     #endregion
         
     #region Constructor
     public PC_Clube_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
     {
         _pc_DashBoardVM = pc_DashBoardVM;
         CarregarDados();
     }
     #endregion
 
     #region Methods

     private void CarregarDados()
     {
         try
         {
             ListaClube = new List<ClubeModel>();
             var clubeRepository = new ClubeRepository();
             ListaClube = clubeRepository.GetAll();
             for (int i = 0; i < ListaClube.Count; i++)
                 ListaClube[i].IsEven = (i % 2 == 0);
         }
         catch (Exception e)
         {
             Console.WriteLine(e);
             throw;
         }
     }
     
     private void CadastrarClubeExecute()
     {
         _pc_DashBoardVM.AtualizarPage("Cadastro de Clubes");
     }

     private void EditarClubeExecute(ClubeModel clubeModel)
     {
         _pc_DashBoardVM.AtualizarPage("Cadastro de Clubes", clubeModel);
     }

    
     #endregion
     public event PropertyChangedEventHandler PropertyChanged;
     protected void OnPropertyChanged([CallerMemberName] string name = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
 }
    