using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_ConfiguracaoInicial_NovoCampeonato_PartialViewModel : BaseViewModel
{
    
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;

    private string _nomeCampeonato;
    private DateTime _dataSelecionada;
    private string _localCampeonato;
    private List<int> _listaRodadas;
    private int _numeroRodadasSelecionada;
    private List<int> _listaCampos;
    private int _numeroCamposSelecionado;
    #endregion

    #region Properties
    public string NomeCampeonato { get => _nomeCampeonato; set => SetProperty(ref _nomeCampeonato, value); }
    public string LocalCampeonato { get => _localCampeonato; set => SetProperty(ref _localCampeonato, value); }
    public int NumeroRodadasSelecionada { get => _numeroRodadasSelecionada; set => SetProperty(ref _numeroRodadasSelecionada, value); }
    public DateTime DataSelecionada { get => _dataSelecionada; set => SetProperty(ref _dataSelecionada, value); }
    public int NumeroCamposSelecionado { get => _numeroCamposSelecionado; set => SetProperty(ref _numeroCamposSelecionado, value); }
    public List<int> ListaRodadas { get => _listaRodadas; set => SetProperty(ref _listaRodadas, value); }
    public List<int> ListaCampos { get => _listaCampos; set => SetProperty(ref _listaCampos, value); }
    #endregion
    
    #region Commands
    public ICommand VoltarCommand => new Command(() => VoltarExecute());
    public ICommand ConfigurarCampeonatoCommand => new Command(() => ConfigurarCampeonatoExecute());
    #endregion
    
    #region Constructor
    public PC_ConfiguracaoInicial_NovoCampeonato_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        
        CarregarDados(pc_DashBoardVM);
    }
    #endregion

    #region Methods

    private void CarregarDados(PC_DashBoardViewModel pc_DashBoardVM)
    {
        try
        {
            _pc_DashBoardVM = pc_DashBoardVM;
            DataSelecionada = DateTime.Now;
            ListaCampos = new List<int>() { 0, 1, 2, 3, 4, 5 };
            ListaRodadas = new List<int>() { 0, 1, 2, 3, 4, 5 };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private void VoltarExecute()
    {
        _pc_DashBoardVM.AtualizarPage("DashBoard");
    }

    private async void ConfigurarCampeonatoExecute()
    {
        try
        {
            var resposta = await Application.Current.MainPage.DisplayAlert("Atenção",
                "Ao iniciar um novo campeonato, o anterior será apagado, Confirma?", "Sim", "Nao");
            if (resposta)
            {
                if (string.IsNullOrEmpty(NomeCampeonato) || string.IsNullOrEmpty(LocalCampeonato) ||
                    NumeroRodadasSelecionada == 0 || NumeroCamposSelecionado == 0 ||
                    DataSelecionada.Date < DateTime.Now.Date)
                {
                    Application.Current.MainPage.DisplayAlert("Atenção", "É necessário preencher todos os campos!", "OK");
                }
                else
                {
                    var campeonato = new CampeonatoModel();
                    campeonato.Campeonato_Data = DataSelecionada;
                    campeonato.Campeonato_Nome = NomeCampeonato;
                    campeonato.Campeonato_NumerosCampos = NumeroCamposSelecionado;
                    campeonato.Campeonato_NumerosRodadas = NumeroRodadasSelecionada;
                    campeonato.Campeonato_Local = LocalCampeonato;

                    var campeonatoRepository = new CampeonatoRepository();
                    campeonatoRepository.InsertOrReplace(campeonato);
                    _pc_DashBoardVM.AtualizarPage("Novo Campeonato", campeonato);
                }
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