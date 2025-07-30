using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroClube_PartialViewModel: BaseViewModel,INotifyPropertyChanged
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private ClubeModel _clube;
    private List<RegionalModel> _regionalList;
    private RegionalModel _regionalSelecionado;
    private string _regional_Nome;
    private string _imagemClube;
    private string _nomeClube;
    private string _presidenteClube;
    private string _telefoneClube;
    private string _emailClube;
    private string _paisClube;
    private string _ufClube;
    private string _cidadeClube;
    private string _bairroClube;
    private string _logradouroClube;
    private bool   _modoEdicao;
    #endregion

    #region Properties
    public bool ModoEdicao { get => _modoEdicao; set => SetProperty(ref _modoEdicao, value); }
    public string ImagemClube
    {
        get => _imagemClube;
        set
        {
            if (_imagemClube != value)
            {
                _imagemClube = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagemClube)));
            }
        }
    }
    public string Regional_Nome
    {
        get => _regional_Nome;
        set
        {
            if (_regional_Nome != value)
            {
                _regional_Nome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regional_Nome)));
            }
        }
    }
    public RegionalModel RegionalSelecionado
    {
        get => _regionalSelecionado;
        set
        {
            if (_regionalSelecionado != value)
            {
                _regionalSelecionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegionalSelecionado)));
            }
        }
    }
    public ClubeModel Clube { get => _clube; set => SetProperty(ref _clube, value); }
    public List<RegionalModel> RegionalList { get => _regionalList; set => SetProperty(ref _regionalList, value); }
    public string NomeClube { get => _nomeClube; set => SetProperty(ref _nomeClube, value); }
    public string PresidenteClube { get => _presidenteClube; set => SetProperty(ref _presidenteClube, value); }
    public string TelefoneClube { get => _telefoneClube; set => SetProperty(ref _telefoneClube, value); }
    public string EmailClube { get => _emailClube; set => SetProperty(ref _emailClube, value); }
    public string PaisClube { get => _paisClube; set => SetProperty(ref _paisClube, value); }
    public string UFClube { get => _ufClube; set => SetProperty(ref _ufClube, value); }
    public string CidadeClube { get => _cidadeClube; set => SetProperty(ref _cidadeClube, value); }
    public string BairroClube { get => _bairroClube; set => SetProperty(ref _bairroClube, value); }
    public string LogradouroClube { get => _logradouroClube; set => SetProperty(ref _logradouroClube, value); }
    #endregion
    
    #region Commands
    public ICommand AdicionarImagemClubeCommand => new Command(() => AdicionarImagemClubeExecute());
    public ICommand AdicionarClubeCommand => new Command(() => AdicionarClubeExecute());
    public ICommand VoltarClubeCommand => new Command(() => VoltarClubeExecute());
    #endregion
    
    #region Constructor
    public PC_CadastroClube_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, ClubeModel clubeModel = null,  bool modoEdicao = false)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados(clubeModel, modoEdicao);
    }
    #endregion

    #region Methods

    private void CarregarDados(ClubeModel clube, bool modoEdicao = false)
    {
        try
        {
            ModoEdicao = modoEdicao;
            RegionalList = new List<RegionalModel>();
            var regionalRepository = new RegionalRepository();
            RegionalList = regionalRepository.GetAll();

            if (clube == null)
            {
                Clube = new ClubeModel();
                ImagemClube = "sem_imagem.jpeg";
            }
            else
            {
                Clube = clube;
                NomeClube = clube.Clube_Nome;
                PresidenteClube = clube.Clube_Presidente;
                TelefoneClube = clube.Clube_Telefone;
                EmailClube = clube.Clube_Email;
                PaisClube = clube.Clube_Pais;
                UFClube = clube.Clube_UF;
                CidadeClube = clube.Clube_Cidade;
                BairroClube = clube.Clube_Bairro;
                LogradouroClube = clube.Clube_Logradouro;
                ImagemClube = clube.Clube_Logo;
                var regional = regionalRepository.GetById(clube.FK_Regional_Id);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RegionalSelecionado =  regional;
                    Regional_Nome = regional.Regional_Nome;
                    OnPropertyChanged();
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private async void AdicionarClubeExecute()
    {
        try
        {
            var clubeRepository = new ClubeRepository();
            if (!string.IsNullOrEmpty(NomeClube))
            {
                Clube.FK_Regional_Id = RegionalSelecionado.Id;
                Clube.Clube_Logo = ImagemClube;
                Clube.Clube_Nome = NomeClube;
                Clube.Clube_Presidente = PresidenteClube;
                Clube.Clube_Telefone = TelefoneClube;
                Clube.Clube_Email = EmailClube;
                Clube.Clube_Pais = PaisClube;
                Clube.Clube_UF = UFClube;
                Clube.Clube_Cidade = CidadeClube;
                Clube.Clube_Bairro = BairroClube;
                Clube.Clube_Logradouro = LogradouroClube;
                clubeRepository.InsertOrReplace(Clube);
                await Application.Current.MainPage.DisplayAlert("Atenção", "Cadastro efetuado com sucesso!", "Sim");
                _pc_DashBoardVM.AtualizarPage("Lista de Clubes");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async void AdicionarImagemClubeExecute()
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

            ImagemClube = destino;
            OnPropertyChanged();
            Console.WriteLine($"Imagem salva em: {destino}");
        }

    }

    private void VoltarClubeExecute()
    {
        _pc_DashBoardVM.AtualizarPage("Lista de Clubes");
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

}