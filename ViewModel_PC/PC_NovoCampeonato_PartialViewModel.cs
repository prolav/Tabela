using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_NovoCampeonato_PartialViewModel:BaseViewModel
{
    #region Model Auxiliar
    public class GeradorNomeTime
    {
        public string NomeClube { get; set; }
        public int NumeroClube { get; set; } = 0;
    }

    public class VerificadorNomeTimeIgual
    {
        public string NomeTime { get; set; }
    }
    
    #endregion

    #region Fields
    private List<GeradorNomeTime>  _listGeradorNomeTime  = new List<GeradorNomeTime>();

    private List<VerificadorNomeTimeIgual> _listVerificadorNomeTimeIgual;
    //private List<JogadorModel> _listaJogador;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<ClubeModel>  _listaClube;
    private int _numeroCampos;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel1;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel2;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel3;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel4;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel5;
    private CampeonatoModel _campeonato;
    #endregion

    #region Properties
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel1 { get => _listmontagemCampeonatoModel1; set => SetProperty(ref _listmontagemCampeonatoModel1, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel2 { get => _listmontagemCampeonatoModel2; set => SetProperty(ref _listmontagemCampeonatoModel2, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel3 { get => _listmontagemCampeonatoModel3; set => SetProperty(ref _listmontagemCampeonatoModel3, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel4 { get => _listmontagemCampeonatoModel4; set => SetProperty(ref _listmontagemCampeonatoModel4, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel5 { get => _listmontagemCampeonatoModel5; set => SetProperty(ref _listmontagemCampeonatoModel5, value); }
    public List<ClubeModel> ListaClube { get => _listaClube; set => SetProperty(ref _listaClube, value); }
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    
    public int NumeroCampos
    {
        get => _numeroCampos;
        set
        {
            _numeroCampos = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(_campeonato.Campeonato_NumerosCampos));
        }
    }
    #endregion
            
    #region Commands
    public ICommand LimparTudoCommand => new Command(() => LimparTudoExecute());
    public ICommand GerarNomesCommand => new Command(() => CarregarGeradorNomesTimesExecute());
    public ICommand AdicionarNovoCampeonatoCommand => new Command(() => AdicionarNovoCampeonatoExecute());
    #endregion
            
    #region Constructor
    public PC_NovoCampeonato_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, CampeonatoModel campeonatoModel)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados(campeonatoModel);
    }
    #endregion

    #region Methods

    private void CarregarDados(CampeonatoModel campeonato)
    {
        try
        {
            Campeonato = campeonato;
            GeradorListaCampos();
            CarregarCorLista();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async void AdicionarNovoCampeonatoExecute()
    {
        try
        {
            CarregarVerificadorQtdeTimesPorCampo();
            CarregarVerificadorNomesIguais();
            var resposta = await Application.Current.MainPage.DisplayAlert("Atenção", $"Confirma todos as informações?", "OK", "Cancelar");
            if (resposta)
                CriarCampeonato();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void CriarCampeonato()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                VerificadorNomesIguais(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                VerificadorNomesIguais(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                VerificadorNomesIguais(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                VerificadorNomesIguais(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                VerificadorNomesIguais(ListmontagemCampeonatoModel5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CriarTodosCamposCampeonato()
    {
        try
        {

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarVerificadorNomesIguais()
    {
        try
        {
            _listVerificadorNomeTimeIgual = new List<VerificadorNomeTimeIgual>();
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                VerificadorNomesIguais(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                VerificadorNomesIguais(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                VerificadorNomesIguais(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                VerificadorNomesIguais(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                VerificadorNomesIguais(ListmontagemCampeonatoModel5);

            var nomesDuplicados = _listVerificadorNomeTimeIgual
                .GroupBy(x => x.NomeTime.Trim().ToLower())
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (nomesDuplicados.Count > 0)
                throw new Exception($"O nome do Time {nomesDuplicados[0]} está repetido");
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void VerificadorNomesIguais(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            foreach (var montagemCampeonato in listaMontagemCampeonato)
            {
                if (montagemCampeonato.Clube != null)
                {
                    var geradorTime = new VerificadorNomeTimeIgual()
                    {
                        NomeTime = montagemCampeonato.Apelido_Time,
                    };  
                    _listVerificadorNomeTimeIgual.Add(geradorTime);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void CarregarGeradorNomesTimesExecute()
    {
        try
        {
            _listGeradorNomeTime = new List<GeradorNomeTime>();
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                GeradorNomesTimes(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                GeradorNomesTimes(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                GeradorNomesTimes(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                GeradorNomesTimes(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                GeradorNomesTimes(ListmontagemCampeonatoModel5);
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private void GeradorNomesTimes(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            foreach (var lista1 in listaMontagemCampeonato)
            {
                if (string.IsNullOrEmpty(lista1.Apelido_Time) && lista1.Clube != null)
                {
                    var geradorTime = new GeradorNomeTime()
                    {
                        NomeClube = lista1.Clube.Clube_Nome,
                        NumeroClube = BuscarUltimoNumeroClube(lista1.Clube.Clube_Nome) + 1
                    };  
                    _listGeradorNomeTime.Add(geradorTime);

                    lista1.Apelido_Time = lista1.Clube.Clube_Nome + " " + geradorTime.NumeroClube.ToString();
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private int BuscarUltimoNumeroClube(string nomeClube)
    {
        try
        {
            var retorno = 0;
            foreach (var lista in _listGeradorNomeTime)
            {
                if (lista.NomeClube == nomeClube)
                {
                    if (lista.NumeroClube > retorno)
                        retorno = lista.NumeroClube;
                }
            }
            return retorno;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void VerificadorQtdeTimesPorCampo(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            var contadorTimesCampo1 = listaMontagemCampeonato
                .Where(a => a.Clube != null).Count();

            if (contadorTimesCampo1 <= Campeonato.Campeonato_NumerosRodadas && listaMontagemCampeonato[0].CampoHabilitado == true)
                throw new Exception($"Número clubes no campo {listaMontagemCampeonato[0].MontagemCampeonatoModel_NumeroCampo} tem que ser superior ao número de rodadas {Campeonato.Campeonato_NumerosRodadas}");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarVerificadorQtdeTimesPorCampo()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                VerificadorQtdeTimesPorCampo(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                VerificadorQtdeTimesPorCampo(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                VerificadorQtdeTimesPorCampo(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                VerificadorQtdeTimesPorCampo(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                VerificadorQtdeTimesPorCampo(ListmontagemCampeonatoModel5);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void CarregarCorLista()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                CarregarCorCard(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                CarregarCorCard(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                CarregarCorCard(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                CarregarCorCard(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                CarregarCorCard(ListmontagemCampeonatoModel5);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private void CarregarCorCard(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            for (int i = 0; i < listaMontagemCampeonato.Count; i++)
                listaMontagemCampeonato[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void GeradorListaCampos()
    {
        try
        {
            var clubeRepository = new ClubeRepository();
            ListaClube = new List<ClubeModel>();
            var faseRepository = new FasesRepository();
            var fase = faseRepository.GetAll().Where(a => a.Fase_Nome == "Grupos").ToList();
            var faseId = fase[0].Id;
            ListaClube = clubeRepository.GetAll();
            var montagemCampeonatoRepository = new MontagemCampeonatoRepository();
            ListmontagemCampeonatoModel1 = new List<MontagemCampeonatoModel>();
            ListmontagemCampeonatoModel2 = new List<MontagemCampeonatoModel>();
            ListmontagemCampeonatoModel3 = new List<MontagemCampeonatoModel>();
            ListmontagemCampeonatoModel4 = new List<MontagemCampeonatoModel>();
            ListmontagemCampeonatoModel5 = new List<MontagemCampeonatoModel>();

            if (_campeonato.Campeonato_NumerosCampos >= 1)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var montagemCampeonato = new MontagemCampeonatoModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 1,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListmontagemCampeonatoModel1.Add(montagemCampeonato);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 2)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var montagemCampeonato = new MontagemCampeonatoModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 2,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListmontagemCampeonatoModel2.Add(montagemCampeonato);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 3)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var montagemCampeonato = new MontagemCampeonatoModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 3,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListmontagemCampeonatoModel3.Add(montagemCampeonato);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 4)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var montagemCampeonato = new MontagemCampeonatoModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 4,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListmontagemCampeonatoModel4.Add(montagemCampeonato);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 5)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var montagemCampeonato = new MontagemCampeonatoModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 5,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListmontagemCampeonatoModel5.Add(montagemCampeonato);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void LimparTudoExecute()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                LimparCampos(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                LimparCampos(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                LimparCampos(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                LimparCampos(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                LimparCampos(ListmontagemCampeonatoModel5);
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void LimparCampos(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            foreach (var listMontagemCampeonato in listaMontagemCampeonato)
            {
                listMontagemCampeonato.Clube = null;
                string.IsNullOrEmpty(listMontagemCampeonato.Apelido_Time);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    #endregion

    #region Helpers
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}