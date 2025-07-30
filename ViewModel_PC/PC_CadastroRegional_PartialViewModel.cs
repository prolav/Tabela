using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroRegional_PartialViewModel: BaseViewModel,INotifyPropertyChanged
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private RegionalModel _regional;
    private string _nomeRegional;
    private string _presidenteRegional;
    private string _telefoneRegional;
    private string _emailRegional;
    private DateTime _dataInicioRegional;
    private DateTime _dataFimRegional;
    private bool _modoEdicao;

    #endregion

    #region Properties
    public string NomeRegional { get => _nomeRegional; set => SetProperty(ref _nomeRegional, value); }
    public string PresidenteRegional  { get => _presidenteRegional; set => SetProperty(ref _presidenteRegional, value); }
    public string TelefoneRegional { get => _telefoneRegional; set => SetProperty(ref _telefoneRegional, value); }
    public string EmailRegional { get => _emailRegional; set => SetProperty(ref _emailRegional, value); }
    public DateTime DataInicioRegional   { get => _dataInicioRegional; set => SetProperty(ref _dataInicioRegional, value); }
    public DateTime DataFimRegional   { get => _dataFimRegional; set => SetProperty(ref _dataFimRegional, value); }
    public RegionalModel Regional { get => _regional; set => SetProperty(ref _regional, value); }
    public bool ModoEdicao  { get => _modoEdicao; set => SetProperty(ref _modoEdicao, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarRegionalCommand => new Command(() => AdicionarRegionalExecute());
    public ICommand VoltarRegionalCommand => new Command(() => VoltarRegionalExecute());
    #endregion
    
    #region Constructor
    public PC_CadastroRegional_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, RegionalModel regional, bool modoEdicao = false)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados(regional, modoEdicao);
    }
    #endregion

    #region Methods

    private void CarregarDados(RegionalModel regional, bool modoEdicao = false)
    {
        try
        {
            ModoEdicao = modoEdicao;
            if (regional == null)
                Regional = new RegionalModel();
            else
            {
                Regional = regional;
                NomeRegional = regional.Regional_Nome;
                PresidenteRegional = regional.Regional_Presidente;
                TelefoneRegional = regional.Regional_Telefone;
                EmailRegional = regional.Regional_Email;
                DataInicioRegional = regional.Regional_Dt_Inicio;
                DataFimRegional =  regional.Regional_Dt_Fim;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private async void AdicionarRegionalExecute()
    {
        try
        {
            var regionalRepository = new RegionalRepository();
            var regional = new RegionalModel();

            if (!string.IsNullOrEmpty(NomeRegional))
            {
                Regional.Regional_Nome = NomeRegional; 
                Regional.Regional_Presidente = PresidenteRegional;
                Regional.Regional_Telefone = TelefoneRegional;
                Regional.Regional_Email = EmailRegional;
                Regional.Regional_Dt_Inicio = DataInicioRegional;
                Regional.Regional_Dt_Fim = DataFimRegional;

                regionalRepository.InsertOrReplace(Regional);
                await Application.Current.MainPage.DisplayAlert("Atenção", "Cadastro efetuado com sucesso!", "OK");
                _pc_DashBoardVM.AtualizarPage("Lista de Regionais");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void VoltarRegionalExecute()
    {
        _pc_DashBoardVM.AtualizarPage("Lista de Regionais");
    }

    #endregion

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}