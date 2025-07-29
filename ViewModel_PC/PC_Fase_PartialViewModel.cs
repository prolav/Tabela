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
                    EditarFaseExecute(FaseSelecionado);
            }
        }
    }
  
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
         
    #region Commands
    public ICommand CadastrarFaseCommand => new Command(() => CadastrarFaseExecute());
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
        _pc_DashBoardVM.AtualizarPage("Cadastro de Fases", fase);
    }
    #endregion
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
