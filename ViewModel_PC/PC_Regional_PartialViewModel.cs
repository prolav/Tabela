using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_Regional_PartialViewModel : BaseViewModel
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<RegionalModel>   _listaRegional;
    private RegionalModel _regionalSelecionado;
    #endregion
 
    #region Properties
    public List<RegionalModel> ListaRegional { get => _listaRegional; set => SetProperty(ref _listaRegional, value); }

    public RegionalModel RegionalSelecionado
    {
        get => _regionalSelecionado;
        set
        {
            if (_regionalSelecionado != value)
            {
                _regionalSelecionado = value;
                OnPropertyChanged();

                if (_regionalSelecionado != null)
                    VisualizarRegionalExecute(_regionalSelecionado);
            }
        }
    }
  
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
         
    #region Commands
    public ICommand CadastrarRegionalCommand => new Command(() => CadastrarRegionalExecute());
    public ICommand EditarRegionalCommand => new Command<object>(
        obj => EditarRegionalExecute(obj as RegionalModel));

    public ICommand ExcluirRegionalCommand => new Command<object>(
        obj => ExcluirRegionalExecute(obj as RegionalModel));
    #endregion
         
    #region Constructor
    public PC_Regional_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
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
            ListaRegional = new List<RegionalModel>();
            var regionalRepository = new RegionalRepository();
            ListaRegional = regionalRepository.GetAll();
            for (int i = 0; i < ListaRegional.Count; i++)
                ListaRegional[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
     
    private void CadastrarRegionalExecute()
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Regionais");
    }

    private void EditarRegionalExecute(RegionalModel regional)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Regionais", regional, true);
    }

    private void VisualizarRegionalExecute(RegionalModel regional)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Regionais", regional, false);
    }

    private async void ExcluirRegionalExecute(RegionalModel regional)
    {
        try
        {
            var listClubes = new List<ClubeModel>();
            var clubRepository = new ClubeRepository();
            listClubes = clubRepository.GetAll();
            bool regionalRelacionado = false;
            foreach (var clubes in listClubes)
            {
                if (clubes.Id == regional.Id)
                    regionalRelacionado = true;
            }

            if (regionalRelacionado == false)
            {
                var resposta = await Application.Current.MainPage.DisplayAlert("Atenção", $"Deseja realmente excluir a regional \"{regional.Regional_Nome}\" ?", "OK", "Cancelar");
                if (resposta == true)
                {
                    var regionalRepository = new RegionalRepository();
                    regionalRepository.Delete(regional);
                    CarregarDados();
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Atenção", $"Não é possível deletar uma regional com clube relacionado", "OK");
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
