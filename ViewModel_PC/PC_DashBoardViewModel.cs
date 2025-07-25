using System.Windows.Input;
using Tabela.View_PC.PC_Partial;

namespace Tabela.ViewModel_PC;

public class PC_DashBoardViewModel: BaseViewModel
{
    #region Fields
    private string _secaoAtual;
    private View? _currentView;
    private enum TipoPage
    {
        Dashboard,
        Tabela,
        PlayOff,
        ClassificacaoGeral,
        HistoricoJogos,
        CadastroJogador,
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

    public View CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value); // Se usar BaseViewModel com SetProperty
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
    
    public ICommand SairCommand { get; }
    #endregion
    
    #region Constructor
    public PC_DashBoardViewModel()
    {
        SairCommand = new Command(SairCommandExecute);
        SecaoAtual = TipoPage.Dashboard.ToString();
        CurrentView = new PC_DashBoard_Partial();
    }
    #endregion

    #region Methods
    private void SairCommandExecute()
    {
        

    }

    private void MudancaPage(TipoPage tipoPage)
    {
        SecaoAtual = tipoPage.ToString();
        if (tipoPage == TipoPage.Dashboard)
        {
            CurrentView = new PC_DashBoard_Partial();
        }
        else if (tipoPage == TipoPage.Tabela)
        {

        }
        else if (tipoPage == TipoPage.PlayOff)
        {

        }
        else if (tipoPage == TipoPage.ClassificacaoGeral)
        {
            
        }
        else if (tipoPage == TipoPage.HistoricoJogos)
        {
            
        }
        else if (tipoPage == TipoPage.CadastroJogador)
        {

        }
        else if (tipoPage == TipoPage.CadastroClube)
        {
            CurrentView = new PC_Clube_Partial();
        }
        
    }
    #endregion
}