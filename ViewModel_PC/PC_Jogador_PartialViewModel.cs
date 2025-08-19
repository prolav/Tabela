using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_Jogador_PartialViewModel : BaseViewModel
{
    #region Fields
    private List<JogadorModel> _listaJogador;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private JogadorModel _jogadorSelecionado;
    #endregion

    #region Properties
    public List<JogadorModel> ListaJogador { get => _listaJogador; set => SetProperty(ref _listaJogador, value); }

    public JogadorModel JogadorSelecionado
    {
        get => _jogadorSelecionado;
        set
        {
            if (_jogadorSelecionado != value)
            {
                _jogadorSelecionado = value;
                OnPropertyChanged();
     
                if (_jogadorSelecionado != null)
                    VisualizarJogadorExecute(_jogadorSelecionado);
            }
        }
    }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion
            
    #region Commands
    public ICommand CadastrarJogadorCommand => new Command(() => CadastrarJogadorExecute());
    public ICommand EditarJogadorCommand => new Command<object>(
        obj => EditarJogadorExecute(obj as JogadorModel));
    public ICommand ExcluirJogadorCommand => new Command<object>(
        obj => ExcluirJogadorExecute(obj as JogadorModel));
    #endregion
            
    #region Constructor
    public PC_Jogador_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        ListaJogador = new List<JogadorModel>();
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados();
    }
    #endregion

    #region Methods

    private void CarregarDados()
    {
        try
        {
            ListaJogador = new List<JogadorModel>();
            var jogadorRepository = new JogadorRepository();
            ListaJogador = jogadorRepository.GetAll();
            CarregarClubes();
            for (int i = 0; i < ListaJogador.Count; i++)
                ListaJogador[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void CarregarClubes()
    {
        try
        {
            var clubeRepository = new ClubeRepository();
            foreach (var jogador in ListaJogador)
            {
                jogador.Clube = clubeRepository.GetById(jogador.Jogador_ClubeId);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void CadastrarJogadorExecute()

    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Jogadores");
    }

    private void EditarJogadorExecute(JogadorModel Jogador)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Jogadores", Jogador, true);
    }

    private void VisualizarJogadorExecute(JogadorModel jogador)
    {
        _pc_DashBoardVM.AtualizarPage("Cadastro de Jogadores", jogador, false);
    }

    private async void ExcluirJogadorExecute(JogadorModel jogador)
    {
        try
        {
            var resposta = await Application.Current.MainPage.DisplayAlert("Atenção", $"Deseja realmente excluir o Jogador \"{jogador.Jogador_Nome}\" ?", "OK", "Cancelar");
            if (resposta == true)
            {
                var jogadorRepository = new JogadorRepository();
                jogadorRepository.Delete(jogador);
                CarregarDados();
            }
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }
    #endregion
}