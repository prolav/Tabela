using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_Fase_PartialViewModel : BaseViewModel
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<FaseModel>   _listaFase;
    private FaseModel _faseSelecionado;
    #endregion
 
    #region Properties
    public List<FaseModel> ListaFase { get => _listaFase; set => SetProperty(ref _listaFase, value); }

    public FaseModel FaseSelecionado
    {
        get => _faseSelecionado;
        set
        {
            if (_faseSelecionado != value)
            {
                _faseSelecionado = value;
                OnPropertyChanged();

                if (_faseSelecionado != null)
                    VisualizarFaseExecute(FaseSelecionado);
            }
        }
    }
  
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
         
    #region Commands
    public ICommand CadastrarFaseCommand => new Command(() => CadastrarFaseExecute());
    public ICommand EditarFaseCommand => new Command<object>(
        obj => EditarFaseExecute(obj as FaseModel));
    public ICommand ExcluirFaseCommand => new Command<object>(
        obj => ExcluirFaseExecute(obj as FaseModel));
    #endregion
         
    #region Constructor
    public PC_Fase_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
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
            ListaFase = new List<FaseModel>();
            var faseRepository = new FasesRepository();
            ListaFase = faseRepository.GetAll();
            for (int i = 0; i < ListaFase.Count; i++)
                ListaFase[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
     
    private void CadastrarFaseExecute()
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Fases");
    }

    private void EditarFaseExecute(FaseModel fase)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Fases", fase, true);
    }

    private void VisualizarFaseExecute(FaseModel fase)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Fases", fase, false);
    }

    private async void ExcluirFaseExecute(FaseModel fase)
    {
        try
        {
            var resposta = await Application.Current.MainPage.DisplayAlert("Atenção", $"Deseja realmente excluir a fase \"{fase.Fase_Nome}\" ?", "OK", "Cancelar");
            if (resposta == true)
            {
                var faseRepository = new FasesRepository();
                faseRepository.Delete(fase);
                CarregarDados();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
