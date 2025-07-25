using System.Windows.Input;
using Tabela.View_PC.PC_Partial;

namespace Tabela.ViewModel_PC;

public class PC_DashBoardViewModel: BaseViewModel
{
    #region Fields
    private string _secaoAtual;
    public PC_DashBoardViewModel _pc_DashBoardViewModel;
    private View? _currentView;
    private string _tituloCard;
    public enum TipoPage
    {
        Dashboard,
        Tabela,
        PlayOff,
        ClassificacaoGeral,
        HistoricoJogos,
        CadastroJogador,
        Clube,
        Jogador,
        CadastroClube,
        NovoCampeonato
    }

    #endregion

    #region Properties
    public string SecaoAtual
    {
        get => _secaoAtual;
        set => SetProperty(ref _secaoAtual, value); // Se usar BaseViewModel com SetProperty
    }

    public string TituloCard{
        get => _tituloCard;
        set => SetProperty(ref _tituloCard, value); // Se usar BaseViewModel com SetProperty
    }
    public View CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value); // Se usar BaseViewModel com SetProperty
    }

    public PC_DashBoardViewModel PC_DashBoardVM 
    {
        get => _pc_DashBoardViewModel;
        set => SetProperty(ref _pc_DashBoardViewModel, value); // Se usar BaseViewModel com SetProperty
    }

    #endregion
    
    #region Commands

    public ICommand MostrarDashboardCommand => new Command(() => MudancaPage(TipoPage.Dashboard));
    public ICommand MostrarTabelaCommand => new Command(() => MudancaPage(TipoPage.Tabela));
    public ICommand MostrarPlayOffCommand => new Command(() => MudancaPage(TipoPage.PlayOff));
    public ICommand MostrarClassificacaoGeralCommand => new Command(() => MudancaPage(TipoPage.ClassificacaoGeral));
    public ICommand MostrarHistoricoJogosCommand => new Command(() => MudancaPage(TipoPage.HistoricoJogos));
    public ICommand MostrarNovoCampeonatoCommand => new Command(() => MudancaPage(TipoPage.NovoCampeonato));
    public ICommand MostrarCadastroJogadorCommand => new Command(() => MudancaPage(TipoPage.CadastroJogador));
    public ICommand MostrarCadastroClubeCommand => new Command(() => MudancaPage(TipoPage.CadastroClube));
    public ICommand MostrarClubeCommand => new Command(() => MudancaPage(TipoPage.Clube));
    public ICommand MostrarJogadorCommand => new Command(() => MudancaPage(TipoPage.Jogador));
    
    public ICommand SairCommand { get; }
    #endregion
    
    #region Constructor
    public PC_DashBoardViewModel()
    {
        SairCommand = new Command(SairCommandExecute);
        SecaoAtual = TipoPage.Dashboard.ToString();
        PC_DashBoardVM = this;
        MudancaPage(TipoPage.Dashboard);
    }
    #endregion

    #region Methods

    public void AtualizarPage()
    {

    }
    private void SairCommandExecute()
    {
        

    }

    public void MudancaPage(TipoPage tipoPage)
    {
        SecaoAtual = tipoPage.ToString();
        if (tipoPage == TipoPage.Dashboard)
        {
            TituloCard = "DashBoard";
            CurrentView = new PC_DashBoard_Partial(PC_DashBoardVM);
        }
        else if (tipoPage == TipoPage.Tabela)
        {
            TituloCard = "Tabela";
        }
        else if (tipoPage == TipoPage.PlayOff)
        {
            TituloCard = "Mata-Mata";
        }
        else if (tipoPage == TipoPage.ClassificacaoGeral)
        {
            TituloCard = "Classificação Geral";
        }
        else if (tipoPage == TipoPage.HistoricoJogos)
        {
            TituloCard = "Histórico de Jogos";
        }
        else if (tipoPage == TipoPage.CadastroClube)
        {
            TituloCard = "Cadastro de Clubes";
            SecaoAtual = TipoPage.Clube.ToString();
            CurrentView = new PC_CadastroClube_Partial(PC_DashBoardVM);
        }
        else if (tipoPage == TipoPage.Clube)
        {
            TituloCard = "Lista de Clubes";
            CurrentView = new PC_Clube_Partial(PC_DashBoardVM);
        }
        else if (tipoPage == TipoPage.Jogador)
        {
            TituloCard = "Lista de Jogadores";
            //CurrentView = new PC_CadastroClube_Partial(PC_DashBoardVM);
        }
    }
    #endregion
}