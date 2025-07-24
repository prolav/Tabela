using System.Windows.Input;

namespace Tabela.ViewModel_PC;

public class PC_DashBoardViewModel: BaseViewModel
{
    #region Fields
    private string _secaoAtual;

    private enum TipoPage
    {
        Dashboard,
        Tabela,
        PlayOff,
        ClassificacaoGeral,
        HistoricoJogos,
        NovoCampeonato
    }

    #endregion

    #region Properties
    public string SecaoAtual
    {
        get => _secaoAtual;
        set => SetProperty(ref _secaoAtual, value); // Se usar BaseViewModel com SetProperty
    }
    #endregion
    
    #region Commands

    public ICommand MostrarDashboardCommand => new Command(() => MudancaPage(TipoPage.Dashboard));
    public ICommand MostrarTabelaCommand => new Command(() => MudancaPage(TipoPage.Tabela));
    public ICommand MostrarPlayOffCommand => new Command(() => MudancaPage(TipoPage.PlayOff));
    public ICommand MostrarClassificacaoGeralCommand => new Command(() => MudancaPage(TipoPage.ClassificacaoGeral));
    public ICommand MostrarHistoricoJogosCommand => new Command(() => MudancaPage(TipoPage.HistoricoJogos));
    public ICommand MostrarNovoCampeonatoCommand => new Command(() => MudancaPage(TipoPage.NovoCampeonato));
    
    public ICommand SairCommand { get; }
    #endregion
    
    #region Constructor
    public PC_DashBoardViewModel()
    {
        SairCommand = new Command(SairCommandExecute);
        SecaoAtual = TipoPage.Dashboard.ToString();
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
        else if (tipoPage == TipoPage.NovoCampeonato)
        {

        }
        
    }
    #endregion
}