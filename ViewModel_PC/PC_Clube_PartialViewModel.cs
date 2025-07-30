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
                     VisualizarClubeExecute(_clubeSelecionado);
             }
         }
     }

     #endregion
         
     #region Commands
     public ICommand CadastrarClubeCommand => new Command(() => CadastrarClubeExecute());
     public ICommand EditarClubeCommand => new Command<object>(
         obj => EditarClubeExecute(obj as ClubeModel));

     public ICommand ExcluirClubeCommand => new Command<object>(
         obj => ExcluirClubeExecute(obj as ClubeModel));

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
             foreach (var regiao in ListaClube)
             {
                 var regiaoRepository = new RegionalRepository();
                 regiao.Regional = regiaoRepository.GetById(regiao.FK_Regional_Id);
             }
             for (int i = 0; i < ListaClube.Count; i++)
                 ListaClube[i].IsEven = (i % 2 == 0);
         }
         catch (Exception e)
         {
             Console.WriteLine(e);
             throw;
         }
     }
     private void VisualizarClubeExecute(ClubeModel clube)
     {
         if (clube != null)
         {
             _pc_DashBoardVM.AtualizarPage("Cadastro de Clubes", clube, false);
         }
     }
     private void EditarClubeExecute(ClubeModel clube)
     {
         if (clube != null)
         {
             _pc_DashBoardVM.AtualizarPage("Cadastro de Clubes", clube, true);
         }
     }
     private async void ExcluirClubeExecute(ClubeModel clube)
     {
         try
         {
             var resposta = await Application.Current.MainPage.DisplayAlert("Atenção", $"Deseja realmente excluir o clube \"{clube.Clube_Nome}\" ?", "OK", "Cancelar");
             if (resposta == true)
             {
                 var clubeRepository = new ClubeRepository();
                 clubeRepository.Delete(clube);
                 CarregarDados();
             }
         }
         catch (Exception e)
         {
             Console.WriteLine(e);
             throw;
         }
     }
     
     private void CadastrarClubeExecute()
     {
         _pc_DashBoardVM.AtualizarPage("Cadastro de Clubes",null, false);
     }

     #endregion
     public event PropertyChangedEventHandler PropertyChanged;
     protected void OnPropertyChanged([CallerMemberName] string name = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
 }
    