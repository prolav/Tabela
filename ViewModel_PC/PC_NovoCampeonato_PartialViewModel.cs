using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_NovoCampeonato_PartialViewModel : BaseViewModel
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

    private List<GeradorNomeTime> _listGeradorNomeTime = new List<GeradorNomeTime>();

    private List<VerificadorNomeTimeIgual> _listVerificadorNomeTimeIgual;

    //private List<JogadorModel> _listaJogador;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<ClubeModel> _listaClube;
    private int _numeroCampos;
    private List<TimeModel> _listaTime1;
    private List<TimeModel> _listaTime2;
    private List<TimeModel> _listaTime3;
    private List<TimeModel> _listaTime4;
    private List<TimeModel> _listaTime5;
    private CampeonatoModel _campeonato;

    #endregion

    #region Properties

    public List<TimeModel> ListaTime1
    {
        get => _listaTime1;
        set => SetProperty(ref _listaTime1, value);
    }

    public List<TimeModel> ListaTime2
    {
        get => _listaTime2;
        set => SetProperty(ref _listaTime2, value);
    }

    public List<TimeModel> ListaTime3
    {
        get => _listaTime3;
        set => SetProperty(ref _listaTime3, value);
    }

    public List<TimeModel> ListaTime4
    {
        get => _listaTime4;
        set => SetProperty(ref _listaTime4, value);
    }

    public List<TimeModel> ListaTime5
    {
        get => _listaTime5;
        set => SetProperty(ref _listaTime5, value);
    }

    public List<ClubeModel> ListaClube
    {
        get => _listaClube;
        set => SetProperty(ref _listaClube, value);
    }

    public CampeonatoModel Campeonato
    {
        get => _campeonato;
        set => SetProperty(ref _campeonato, value);
    }

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
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private async void AdicionarNovoCampeonatoExecute()
    {
        try
        {
            CarregarVerificadorQtdeTimesPorCampo();
            CarregarVerificadorNomesIguais();
            var resposta =
                await Application.Current.MainPage.DisplayAlert("Atenção", $"Confirma todos as informações?", "OK",
                    "Cancelar");
            if (resposta)
            {
                CarregarRemoverMontagemCampeonatoNulos();
                CarregarCadastrarTodosMontagemCampeonatoEmUso();
                CarregarMontarJogos();
                var campeonatoRepository = new CampeonatoRepository();
                Campeonato.Campeonato_Cadastrado = true;
                campeonatoRepository.InsertOrReplace(Campeonato);
                _pc_DashBoardVM.AtualizarPage("Histórico de Jogos");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void CarregarCadastrarTodosMontagemCampeonatoEmUso()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                CadastrarTodosMontagemCampeonatoEmUso(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                CadastrarTodosMontagemCampeonatoEmUso(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                CadastrarTodosMontagemCampeonatoEmUso(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                CadastrarTodosMontagemCampeonatoEmUso(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                CadastrarTodosMontagemCampeonatoEmUso(ListaTime5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CadastrarTodosMontagemCampeonatoEmUso(List<TimeModel> listaTime)
    {
        try
        {
            var timeRepository = new TimeRepository();
            foreach (var time in listaTime)
            {
                time.FK_Clube_Id = time.Clube.Id;
                timeRepository.InsertOrReplace(time);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private async void GeradorPartidas(List<PartidaModel> listaPartidas)
    {
        try
        {
            var partidaRepository = new PartidaRepository();
            var listAuxPartida = new List<PartidaModel>();
            listaPartidas = completeTime(listaPartidas);
            foreach (var partida in listaPartidas)
            {
                partida.Partida_ComJogo = true;
                partidaRepository.InsertOrReplace(partida);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private List<PartidaModel> completeTime(List<PartidaModel> listaPartidas)
    {
        try
        {
            var timerepository = new TimeRepository();
            foreach (var item in listaPartidas)
            {
                item.TimeCasa = timerepository.GetById(item.TimeCasaId);
                item.TimeFora = timerepository.GetById(item.TimeForaId);
            }
            return listaPartidas;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarMontarJogos()
    {
        try
        {
            var listapartidas = new List<PartidaModel>();
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                listapartidas.AddRange(MontarJogos(ListaTime1));
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                listapartidas.AddRange(MontarJogos(ListaTime2));
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                listapartidas.AddRange(MontarJogos(ListaTime3));
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                listapartidas.AddRange(MontarJogos(ListaTime4));
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                listapartidas.AddRange(MontarJogos(ListaTime5));

            GeradorPartidas(listapartidas);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarRemoverMontagemCampeonatoNulos()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                RemoverMontagemCampeonatoNulos(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                RemoverMontagemCampeonatoNulos(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                RemoverMontagemCampeonatoNulos(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                RemoverMontagemCampeonatoNulos(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                RemoverMontagemCampeonatoNulos(ListaTime5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void RemoverMontagemCampeonatoNulos(List<TimeModel> listaTime)
    {
        try
        {
            listaTime.RemoveAll(item => item.Clube == null);
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
                VerificadorNomesIguais(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                VerificadorNomesIguais(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                VerificadorNomesIguais(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                VerificadorNomesIguais(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                VerificadorNomesIguais(ListaTime5);

            var nomesDuplicados = _listVerificadorNomeTimeIgual
                .GroupBy(x => x.NomeTime.Trim().ToLower())
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (nomesDuplicados.Count > 0)
            {
                if (string.IsNullOrEmpty(nomesDuplicados[0]))
                {
                    nomesDuplicados[0] = "{sem nome}";
                }

                throw new Exception($"O nome do Time {nomesDuplicados[0]} está repetido");
            }

            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void VerificadorNomesIguais(List<TimeModel> listaTime)
    {
        try
        {
            foreach (var time in listaTime)
            {
                if (time.Clube != null)
                {
                    var geradorTime = new VerificadorNomeTimeIgual()
                    {
                        NomeTime = time.Apelido_Time,
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
                GeradorNomesTimes(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                GeradorNomesTimes(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                GeradorNomesTimes(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                GeradorNomesTimes(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                GeradorNomesTimes(ListaTime5);
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void GeradorNomesTimes(List<TimeModel> listaTime)
    {
        try
        {
            foreach (var lista1 in listaTime)
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

    private void VerificadorQtdeTimesPorCampo(List<TimeModel> listaTime)
    {
        try
        {
            var contadorTimesCampo1 = listaTime
                .Where(a => a.Clube != null).Count();

            if (contadorTimesCampo1 <= Campeonato.Campeonato_NumerosRodadas && listaTime[0].CampoHabilitado == true)
                throw new Exception(
                    $"Número clubes no campo {listaTime[0].MontagemCampeonatoModel_NumeroCampo} tem que ser superior ao número de rodadas {Campeonato.Campeonato_NumerosRodadas}");
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
                VerificadorQtdeTimesPorCampo(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                VerificadorQtdeTimesPorCampo(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                VerificadorQtdeTimesPorCampo(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                VerificadorQtdeTimesPorCampo(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                VerificadorQtdeTimesPorCampo(ListaTime5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarCorLista()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                CarregarCorCard(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                CarregarCorCard(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                CarregarCorCard(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                CarregarCorCard(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                CarregarCorCard(ListaTime5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarCorCard(List<TimeModel> listaTime)
    {
        try
        {
            for (int i = 0; i < listaTime.Count; i++)
                listaTime[i].IsEven = (i % 2 == 0);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
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
            var timeRepository = new TimeRepository();
            ListaTime1 = new List<TimeModel>();
            ListaTime2 = new List<TimeModel>();
            ListaTime3 = new List<TimeModel>();
            ListaTime4 = new List<TimeModel>();
            ListaTime5 = new List<TimeModel>();

            if (_campeonato.Campeonato_NumerosCampos >= 1)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var time = new TimeModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 1,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListaTime1.Add(time);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 2)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var time = new TimeModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 2,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListaTime2.Add(time);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 3)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var time = new TimeModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 3,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListaTime3.Add(time);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 4)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var time = new TimeModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 4,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListaTime4.Add(time);
                }
            }

            if (_campeonato.Campeonato_NumerosCampos >= 5)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var time = new TimeModel()
                    {
                        FK_Campeonato_Id = _campeonato.Id,
                        MontagemCampeonatoModel_NumeroCampo = 5,
                        FK_Fase_Id = faseId,
                        CampoHabilitado = true,
                        NumeroClubeNaqueleCampo = i,
                        ListaClube = _listaClube
                    };
                    ListaTime5.Add(time);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void LimparTudoExecute()
    {
        try
        {
            if (_campeonato.Campeonato_NumerosCampos >= 1)
                LimparCampos(ListaTime1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                LimparCampos(ListaTime2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                LimparCampos(ListaTime3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                LimparCampos(ListaTime4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                LimparCampos(ListaTime5);
            OnPropertyChanged();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void LimparCampos(List<TimeModel> listaTime)
    {
        try
        {
            foreach (var time in listaTime)
            {
                time.Clube = null;
                string.IsNullOrEmpty(time.Apelido_Time);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion

    #region Helpers

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    #endregion


    #region MyRegion
    public List<PartidaModel> MontarJogos(List<TimeModel> times)
    {
        var campeonatoId = Campeonato.Id;
        var faseId = times[0].FK_Fase_Id;
        var partidas = new List<PartidaModel>();
        var totalTimes = times.Count;

        if (totalTimes != 5 && totalTimes != 6)
            throw new InvalidOperationException("Este gerador só funciona para campeonatos com 5 ou 6 times.");

        // Garantir ordem fixa para previsibilidade
        //times = times.OrderBy(t => t.Clube.Clube_Nome).ToList();

        if (totalTimes == 5)
        {
            // ======= Lógica para 5 times =======
            // Rodada 1
            partidas.Add(CriarPartida(1, times[1], times[0], times[4], campeonatoId, faseId));
            partidas.Add(CriarPartida(2, times[3], times[2], times[1], campeonatoId, faseId));
            // Rodada 2
            partidas.Add(CriarPartida(3, times[0], times[4], times[3], campeonatoId, faseId));
            partidas.Add(CriarPartida(4, times[2], times[1], times[0], campeonatoId, faseId));
            // Rodada 3
            partidas.Add(CriarPartida(5, times[4], times[3], times[2], campeonatoId, faseId));
            partidas.Add(CriarPartida(6, times[0], times[2], times[4], campeonatoId, faseId));
            // Rodada 4
            partidas.Add(CriarPartida(7, times[1], times[3], times[0], campeonatoId, faseId));
            partidas.Add(CriarPartida(8, times[2], times[4], times[1], campeonatoId, faseId));
            // Rodada 5
            partidas.Add(CriarPartida(9, times[3], times[0], times[2], campeonatoId, faseId));
            partidas.Add(CriarPartida(10, times[4], times[1], times[3], campeonatoId, faseId));
        }
        else if (totalTimes == 6)
        {
            // ======= Lógica para 6 times =======
            // Rodada 1
            partidas.Add(CriarPartida(1, times[1], times[0], times[5], campeonatoId, faseId));
            partidas.Add(CriarPartida(2, times[3], times[2], times[1], campeonatoId, faseId));
            partidas.Add(CriarPartida(3, times[4], times[5], times[3], campeonatoId, faseId));
            // Rodada 2
            partidas.Add(CriarPartida(4, times[2], times[1], times[4], campeonatoId, faseId));
            partidas.Add(CriarPartida(5, times[5], times[0], times[2], campeonatoId, faseId));
            partidas.Add(CriarPartida(6, times[4], times[3], times[2], campeonatoId, faseId));
            // Rodada 3
            partidas.Add(CriarPartida(7, times[0], times[2], times[4], campeonatoId, faseId));
            partidas.Add(CriarPartida(8, times[1], times[3], times[0], campeonatoId, faseId));
            partidas.Add(CriarPartida(9, times[2], times[4], times[1], campeonatoId, faseId));
            // Rodada 4
            partidas.Add(CriarPartida(10, times[3], times[5], times[2], campeonatoId, faseId));
            partidas.Add(CriarPartida(11, times[0], times[4], times[3], campeonatoId, faseId));
            partidas.Add(CriarPartida(12, times[5], times[1], times[0], campeonatoId, faseId));
        }

        // Retornar ordenado por rodada
        return partidas.OrderBy(p => p.Partida_Rodada).ToList();
    }

    private PartidaModel CriarPartida(int rodada, TimeModel casa, TimeModel fora, TimeModel juiz, Guid campeonatoId,
        Guid faseId)
    {
        var timerepository = new TimeRepository();
        return new PartidaModel
        {
            FK_Campeonato_Id = campeonatoId,
            FaseId = faseId,
            Partida_Rodada = rodada,
            Partida_NumeroCampo = casa.MontagemCampeonatoModel_NumeroCampo, // Ajuste se quiser mais campos
            Partida_DataHora = DateTime.Now, // Ajustar data real
            TimeCasaId = casa.Id,
            TimeCasa = timerepository.GetById(casa.Id),
            TimeForaId = fora.Id,
            TimeFora = timerepository.GetById(fora.Id),
            TimeJuizId = juiz.Id,
            TimeJuiz = timerepository.GetById(juiz.Id),
            Partida_ComJogo = false
        };
    }
}



#endregion
