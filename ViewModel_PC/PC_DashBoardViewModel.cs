using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;
using Tabela.View_PC;
using Tabela.View_PC.PC_Partial;

namespace Tabela.ViewModel_PC;

public class PC_DashBoardViewModel: BaseViewModel
{
    #region Fields
    private string _secaoAtual;
    public PC_DashBoardViewModel _pc_DashBoardViewModel;
    private View? _currentView;
    private string _tituloCard;
    private int _rowContentView;
    private int _rowSpanContentView;
    private CampeonatoModel  _campeonatoModel;

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
        NovoCampeonato,
        CadastroGeral
    }

    #endregion

    #region Properties
    public CampeonatoModel Campeonato { get; set; }
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
    public int RowContentView 
    {
        get => _rowContentView;
        set => SetProperty(ref _rowContentView, value); // Se usar BaseViewModel com SetProperty
    }
    public int RowSpanContentView 
    {
        get => _rowSpanContentView;
        set => SetProperty(ref _rowSpanContentView, value); // Se usar BaseViewModel com SetProperty
    }
    #endregion
    
    #region Commands
    
    public ICommand AtualizarPageCommand => new Command<string>(nomePage => AtualizarPage(nomePage));

    #endregion
    
    #region Constructor
    public PC_DashBoardViewModel()
    {
        SecaoAtual = TipoPage.Dashboard.ToString();
        PC_DashBoardVM = this;
        AtualizarPage("DashBoard");
        
    }
    #endregion

    #region Methods

    public void AtualizarPage(string nomePage, object model = null, bool modoEdicao = false)
    {
        try
        {
            TituloCard = nomePage;
            RowContentView = 1;
            RowSpanContentView = 1;
            var campeonatoRepository = new CampeonatoRepository();
            Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            OnPropertyChanged();
            
            if (nomePage == "DashBoard")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_DashBoard_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Tabela de Grupos")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_Tabela_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Mata-Mata")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_PlayOff_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Classificação Geral")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_ClassificacaoGeral_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Histórico de Jogos")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_HistoricoJogos_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Cadastro de Clubes")
            {
                var clubeModel = new ClubeModel();
                clubeModel = model as ClubeModel;
                if (clubeModel != null)
                {
                    if (modoEdicao == false)
                    {
                        TituloCard = "Visualizar Clube";
                        CurrentView = new PC_CadastroClube_Partial(PC_DashBoardVM, clubeModel, false);
                    }
                    else
                    {
                        TituloCard = "Editar Clube";
                        CurrentView = new PC_CadastroClube_Partial(PC_DashBoardVM, clubeModel, true);
                    }
                }
                else
                {
                    CurrentView = new PC_CadastroClube_Partial(PC_DashBoardVM, clubeModel, true);
                }
            }
            else if (nomePage == "Cadastro de Jogadores")
            {
                var jogadorModel = new JogadorModel();
                jogadorModel = model as JogadorModel;
                if (jogadorModel != null)
                {
                    if (modoEdicao == false)
                    {
                        TituloCard = "Visualizar Jogador";
                        CurrentView = new PC_CadastroJogador_Partial(PC_DashBoardVM, jogadorModel, false);
                    }
                    else
                    {
                        TituloCard = "Editar Jogador";
                        CurrentView = new PC_CadastroJogador_Partial(PC_DashBoardVM, jogadorModel, true);
                    }
                }
                else
                {
                    CurrentView = new PC_CadastroJogador_Partial(PC_DashBoardVM, jogadorModel, true);
                }
            }
            else if (nomePage == "Lista de Clubes")
            {
                CurrentView = new PC_Clube_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Lista de Jogadores")
            {
                CurrentView = new PC_Jogador_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Novo Campeonato")
            {
                Campeonato = model as CampeonatoModel;
                if (Campeonato != null)
                {
                    TituloCard = "";
                    RowContentView = 0;
                    RowSpanContentView = 2;
                    CurrentView = new PC_NovoCampeonato_Partial(PC_DashBoardVM, Campeonato);
                }
                else
                {
                    TituloCard = "Configuração Inicial do Campeonato";
                    CurrentView = new PC_ConfiguracaoInicial_NovoCampeonato_Partial(PC_DashBoardVM);
                }

                
            }
            else if (nomePage == "Cadastro Geral")
            {
                SecaoAtual = nomePage;
                CurrentView = new PC_CadastroGeral_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Lista de Regionais")
            {
                CurrentView = new PC_Regional_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Cadastro de Regionais")
            {
                var regionalModel = new RegionalModel();
                regionalModel = model as RegionalModel;
                if (regionalModel != null)
                {
                    if (modoEdicao == false)
                    {
                        TituloCard = "Visualizar Regional";
                        CurrentView = new PC_CadastroRegional_Partial(PC_DashBoardVM, regionalModel, false);
                    }
                    else
                    {
                        TituloCard = "Editar Regional";
                        CurrentView = new PC_CadastroRegional_Partial(PC_DashBoardVM, regionalModel, true);
                    }
                }
                else
                {
                    CurrentView = new PC_CadastroRegional_Partial(PC_DashBoardVM, regionalModel, true);
                }
            }
            else if (nomePage == "Lista de Fases")
            {
                CurrentView = new PC_Fase_Partial(PC_DashBoardVM);
            }
            else if (nomePage == "Cadastro de Fases")
            {
                var faseModel = new FaseModel();
                faseModel = model as FaseModel;
                if (faseModel != null)
                {
                    if (modoEdicao == false)
                    {
                        TituloCard = "Visualizar Fase";
                        CurrentView = new PC_CadastroFase_Partial(PC_DashBoardVM, faseModel, false);
                    }
                    else
                    {
                        TituloCard = "Editar Fase";
                        CurrentView = new PC_CadastroFase_Partial(PC_DashBoardVM, faseModel, true);
                    }
                }
                else
                {
                    CurrentView = new PC_CadastroFase_Partial(PC_DashBoardVM, faseModel, true);
                }
            }
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }
    private void SairCommandExecute()
    {
        

    }

    
    #endregion
}