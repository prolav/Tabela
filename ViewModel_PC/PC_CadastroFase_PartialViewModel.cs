using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroFase_PartialViewModel: BaseViewModel,INotifyPropertyChanged
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private FaseModel _fase;
    private string _nomeFase;
    #endregion

    #region Properties
    public string NomeFase { get => _nomeFase; set => SetProperty(ref _nomeFase, value); }
    public FaseModel Fase { get => _fase; set => SetProperty(ref _fase, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarFaseCommand => new Command(() => AdicionarFaseExecute());
    #endregion
    
    #region Constructor
    public PC_CadastroFase_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, FaseModel fase)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados(fase);
    }
    #endregion

    #region Methods

    private void CarregarDados(FaseModel fase)
    {
        try
        {
            if (fase == null)
                Fase = new FaseModel();
            else
            {
                Fase = fase;
                NomeFase = fase.Fase_Nome;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private async void AdicionarFaseExecute()
    {
        try
        {
            var regionalRepository = new FasesRepository();
            var fase = new FaseModel();

            if (!string.IsNullOrEmpty(NomeFase))
            {
                Fase.Fase_Nome = NomeFase; 

                regionalRepository.InsertOrReplace(Fase);
                await Application.Current.MainPage.DisplayAlert("Atenção", "Cadastro efetuado com sucesso!", "OK");
                _pc_DashBoardVM.AtualizarPage("Lista de Fases");
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