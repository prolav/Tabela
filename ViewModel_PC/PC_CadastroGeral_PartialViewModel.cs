using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_CadastroGeral_PartialViewModel: BaseViewModel
{
    #region Fields
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private bool _cadastroBaseAtivo;
    private bool _cadastroCampeonatoAtivo;
    private CampeonatoModel _campeonato;
    #endregion

    #region Properties
     public bool CadastroBaseAtivo { get => _cadastroBaseAtivo; set => SetProperty(ref _cadastroBaseAtivo, value); }
     public bool CadastroCampeonatoAtivo { get => _cadastroCampeonatoAtivo; set => SetProperty(ref _cadastroCampeonatoAtivo, value); }
    #endregion
    
    #region Commands
    public ICommand AtualizarPageCommand => new Command<string>(
        nomePage => _pc_DashBoardVM.AtualizarPage(nomePage));

    public ICommand CadastroBaseCommand => new Command(() => CadastroBaseExecute());
    public ICommand CadastroCampeonatoCommand => new Command(() => CadastroCampeonatoExecute());
    
    #endregion
    
    #region Constructor
    public PC_CadastroGeral_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados();
    }

    private void CarregarDados()
    {
        try
        {
            var campoRepository = new CampoRepository();
            var campo = campoRepository.GetAll().Count;
            if (campo == 0)
                CadastroBaseAtivo = true;
            else
                CadastroBaseAtivo = false;

            var campeonatoRepository = new CampeonatoRepository();
            _campeonato = campeonatoRepository.GetAll().LastOrDefault();
            if (_campeonato != null && _campeonato.Campeonato_Cadastrado == false)
                CadastroCampeonatoAtivo = true;
            else
                CadastroCampeonatoAtivo = false;

            OnPropertyChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void CadastroCampeonatoExecute()
    {
        try
        {
            _pc_DashBoardVM.AtualizarPage("Novo Campeonato", _campeonato);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async void CadastroBaseExecute()
    {
        try
        {
            var regionalRepository = new RegionalRepository();
            var clubeRepository = new ClubeRepository();
            var campoRepository = new CampoRepository();
            var faseRepository = new FasesRepository();

            var regional = new RegionalModel()
            {
                Regional_Nome = "Seitô",
            };
            regionalRepository.InsertOrReplace(regional);

            regional = new RegionalModel()
            {
                Regional_Nome = "ABC",
            };
            regionalRepository.InsertOrReplace(regional);

            regional = new RegionalModel()
            {
                Regional_Nome = "Capital",
            };
            regionalRepository.InsertOrReplace(regional);

            var clube = new ClubeModel()
            {
                FK_Regional_Id = regionalRepository.GetAll().Where(a => a.Regional_Nome == "Seitô").FirstOrDefault().Id,
                Clube_Nome = "Time",
                Clube_Logo = "sem_imagem.jpeg",
                Clube_Presidente = "Nobuo",
            };
            clubeRepository.InsertOrReplace(clube);

            clube = new ClubeModel()
            {
                FK_Regional_Id = regionalRepository.GetAll().Where(a => a.Regional_Nome == "Seitô").FirstOrDefault().Id,
                Clube_Nome = "Kyoyu",
                Clube_Logo = "sem_imagem.jpeg",
                Clube_Presidente = "Nobuo",
            };
            clubeRepository.InsertOrReplace(clube);

            clube = new ClubeModel()
            {
                FK_Regional_Id = regionalRepository.GetAll().Where(a => a.Regional_Nome == "Seitô").FirstOrDefault().Id,
                Clube_Nome = "Matilde",
                Clube_Logo = "sem_imagem.jpeg",
                Clube_Presidente = "Hatiro Honda",
            };
            clubeRepository.InsertOrReplace(clube);

            clube = new ClubeModel()
            {
                FK_Regional_Id = regionalRepository.GetAll().Where(a => a.Regional_Nome == "Capital").FirstOrDefault()
                    .Id,
                Clube_Nome = "Casa Verde",
                Clube_Logo = "sem_imagem.jpeg",
                Clube_Presidente = "Hatiro Honda",
            };
            clubeRepository.InsertOrReplace(clube);

            clube = new ClubeModel()
            {
                FK_Regional_Id = regionalRepository.GetAll().Where(a => a.Regional_Nome == "Capital").FirstOrDefault()
                    .Id,
                Clube_Nome = "Nenhum",
                Clube_Logo = "sem_imagem.jpeg",
                Clube_Presidente = "",
            };
            clubeRepository.InsertOrReplace(clube);

            var faseModel = new FaseModel()
            {
                Fase_Nome = "Grupos"
            };
            faseRepository.InsertOrReplace(faseModel);

            faseModel = new FaseModel()
            {
                Fase_Nome = "Oitavas"
            };
            faseRepository.InsertOrReplace(faseModel);

            faseModel = new FaseModel()
            {
                Fase_Nome = "Quartas"
            };
            faseRepository.InsertOrReplace(faseModel);

            faseModel = new FaseModel()
            {
                Fase_Nome = "Semi"
            };
            faseRepository.InsertOrReplace(faseModel);

            faseModel = new FaseModel()
            {
                Fase_Nome = "Final"
            };
            faseRepository.InsertOrReplace(faseModel);

            var campoModel = new CampoModel()
            {
                NomeCampo = "Campo 1"
            };
            campoRepository.InsertOrReplace(campoModel);

            campoModel = new CampoModel()
            {
                NomeCampo = "Campo 2"
            };
            campoRepository.InsertOrReplace(campoModel);

            campoModel = new CampoModel()
            {
                NomeCampo = "Campo 3"
            };
            campoRepository.InsertOrReplace(campoModel);

            campoModel = new CampoModel()
            {
                NomeCampo = "Campo 4"
            };
            campoRepository.InsertOrReplace(campoModel);

            campoModel = new CampoModel()
            {
                NomeCampo = "Campo 5"
            };
            campoRepository.InsertOrReplace(campoModel);

            await Application.Current.MainPage.DisplayAlert("Atenção", "Cadastro efetuado com sucesso!", "OK");
            CarregarDados();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion

    #region Methods

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}
