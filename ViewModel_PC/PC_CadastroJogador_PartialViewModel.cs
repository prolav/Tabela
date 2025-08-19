using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroJogador_PartialViewModel : BaseViewModel,INotifyPropertyChanged
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private bool _modoEdicao;
    private string _imagemJogador;
    private ClubeModel _clubeSelecionado;
    private string _clube_Nome;
    private List<ClubeModel> _listaClube;
    private string _nomeJogador;
    private int _numeroJogador;
    private JogadorModel _jogador;
    #endregion

    #region Properties
    public JogadorModel Jogador { get => _jogador; set => SetProperty(ref _jogador, value); }
    public bool ModoEdicao { get => _modoEdicao; set => SetProperty(ref _modoEdicao, value); }
    public string NomeJogador { get => _nomeJogador; set => SetProperty(ref _nomeJogador, value); }
    public int NumeroJogador { get => _numeroJogador; set => SetProperty(ref _numeroJogador, value); }
    public string ImagemJogador
    {
        get => _imagemJogador;
        set
        {
            if (_imagemJogador != value)
            {
                _imagemJogador = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagemJogador)));
            }
        }
    }
    public string Clube_Nome
    {
        get => _clube_Nome;
        set
        {
            if (_clube_Nome != value)
            {
                _clube_Nome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Clube_Nome)));
            }
        }
    }
    public ClubeModel ClubeSelecionado
    {
        get => _clubeSelecionado;
        set
        {
            if (_clubeSelecionado != value)
            {
                _clubeSelecionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClubeSelecionado)));
            }
        }
    }

    public List<ClubeModel> ListaClube { get => _listaClube; set => SetProperty(ref _listaClube, value); }

    #endregion
    
    #region Commands
    public ICommand AdicionarImagemJogadorCommand => new Command(() => AdicionarImagemJogadorExecute());
    public ICommand AdicionarCommand => new Command(() => AdicionarJogadorExecute());
    public ICommand VoltarCommand => new Command(() => VoltarJogadorExecute());
    #endregion
    
    #region Constructor
    public PC_CadastroJogador_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, JogadorModel jogadorModel = null,  bool modoEdicao = false)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        ImagemJogador = "sem_imagem.jpeg";
        CarregarDados(jogadorModel, modoEdicao);
    }
    #endregion

    #region Methods
    private void CarregarDados(JogadorModel jogador, bool modoEdicao = false)
    {
        try
        {
            ModoEdicao = modoEdicao;
            ListaClube = new List<ClubeModel>();
            var clubeRepository = new ClubeRepository();
            ListaClube = clubeRepository.GetAll();
            Jogador = jogador;
            if (jogador == null)
            {
                Jogador = new JogadorModel();
                ImagemJogador = "sem_imagem.jpeg";
            }
            else
            {
                NomeJogador = jogador.Jogador_Nome;
                NumeroJogador = jogador.Jogador_Numero;
                ImagemJogador = jogador.Jogador_Imagem;
                var clube = clubeRepository.GetById(jogador.Jogador_ClubeId);
                ClubeSelecionado =  clube;
                Clube_Nome = clube.Clube_Nome;
                OnPropertyChanged();
            }
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private async void AdicionarJogadorExecute()
    {
        try
        {
            var jogadorRepository = new JogadorRepository();
            if (!string.IsNullOrEmpty(NomeJogador))
            {
                Jogador.Jogador_ClubeId = ClubeSelecionado.Id;
                Jogador.Jogador_Imagem = ImagemJogador;
                Jogador.Jogador_Nome = NomeJogador;
                Jogador.Jogador_Numero = NumeroJogador;
                jogadorRepository.InsertOrReplace(Jogador);
                await Application.Current.MainPage.DisplayAlert("Atenção", "Cadastro efetuado com sucesso!", "OK");
                _pc_DashBoardVM.AtualizarPage("Lista de Jogadores");
            }
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private async void AdicionarImagemJogadorExecute()
    {
        FileResult photo = await MediaPicker.PickPhotoAsync(); // ou .CapturePhotoAsync()

        if (photo != null)
        {
            var nomeArquivo = Path.GetFileName(photo.FullPath);

            // Define destino (por exemplo, pasta AppData do app)
            var destino = Path.Combine(FileSystem.AppDataDirectory, nomeArquivo);

            // Salva uma cópia local
            using var stream = await photo.OpenReadAsync();
            using var novoArquivo = File.OpenWrite(destino);
            await stream.CopyToAsync(novoArquivo);

            ImagemJogador = destino;
            OnPropertyChanged();
            Console.WriteLine($"Imagem salva em: {destino}");
        }
    }

    private void VoltarJogadorExecute()
    {
        _pc_DashBoardVM.AtualizarPage("Lista de Jogadores");
    }
    #endregion
    #region Events
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}