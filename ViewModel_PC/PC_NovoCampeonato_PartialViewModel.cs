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
            {
                CarregarRemoverMontagemCampeonatoNulos();
                CarregarCadastrarTodosMontagemCampeonatoEmUso();
                CarregarMontarJogos();
                var campeonatoRepository = new CampeonatoRepository();
                Campeonato.Campeonato_Cadastrado = true;
                campeonatoRepository.InsertOrReplace(Campeonato);
                _pc_DashBoardVM.AtualizarPage("DashBoard");
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
                CadastrarTodosMontagemCampeonatoEmUso(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                CadastrarTodosMontagemCampeonatoEmUso(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                CadastrarTodosMontagemCampeonatoEmUso(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                CadastrarTodosMontagemCampeonatoEmUso(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                CadastrarTodosMontagemCampeonatoEmUso(ListmontagemCampeonatoModel5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CadastrarTodosMontagemCampeonatoEmUso(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            var montagemCampeonatoRepository = new MontagemCampeonatoRepository();
            foreach (var montagemCampeonato in listaMontagemCampeonato)
            {
                montagemCampeonatoRepository.InsertOrReplace(montagemCampeonato);        
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
            foreach (var partida in listaPartidas)
            {
                partida.FK_Campeonato_Id = Campeonato.Id;
                partida.Partida_DataHora = Campeonato.Campeonato_Data;
                partida.FaseId = partida.TimeCasa.FK_Fase_Id;
                partida.TimeCasaId = partida.TimeCasa.Id;
                partida.TimeForaId = partida.TimeFora.Id;
                partida.TimeJuizId = partida.TimeJuiz.Id;
                partida.Partida_NumeroCampo = partida.TimeCasa.MontagemCampeonatoModel_NumeroCampo;
                partida.Partida_ComJogo = true;
                partidaRepository.InsertOrReplace(partida);
            }
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
                listapartidas.AddRange(MontarJogos(ListmontagemCampeonatoModel1));
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                listapartidas.AddRange(MontarJogos(ListmontagemCampeonatoModel2));
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                listapartidas.AddRange(MontarJogos(ListmontagemCampeonatoModel3));
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                listapartidas.AddRange(MontarJogos(ListmontagemCampeonatoModel4));
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                listapartidas.AddRange(MontarJogos(ListmontagemCampeonatoModel5));

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
                RemoverMontagemCampeonatoNulos(ListmontagemCampeonatoModel1);
            if (_campeonato.Campeonato_NumerosCampos >= 2)
                RemoverMontagemCampeonatoNulos(ListmontagemCampeonatoModel2);
            if (_campeonato.Campeonato_NumerosCampos >= 3)
                RemoverMontagemCampeonatoNulos(ListmontagemCampeonatoModel3);
            if (_campeonato.Campeonato_NumerosCampos >= 4)
                RemoverMontagemCampeonatoNulos(ListmontagemCampeonatoModel4);
            if (_campeonato.Campeonato_NumerosCampos >= 5)
                RemoverMontagemCampeonatoNulos(ListmontagemCampeonatoModel5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void RemoverMontagemCampeonatoNulos(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        try
        {
            listaMontagemCampeonato.RemoveAll(item => item.Clube == null);
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

    #region teste 1
    public List<PartidaModel> MontarJogos1(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        if (listaMontagemCampeonato.Count < 4 || listaMontagemCampeonato.Count > 6)
            throw new ArgumentException("Número de times deve ser entre 4 e 6.");

        if (Campeonato.Campeonato_NumerosRodadas < 3 || Campeonato.Campeonato_NumerosRodadas > 5)
            throw new ArgumentException("Jogos por time deve ser entre 3 e 5.");

        // Resetar contadores para o caso de reuso
        foreach (var time in listaMontagemCampeonato)
        {
            time.Jogos = 0;
            time.Juiz = 0;
        }

        var jogos = new List<PartidaModel>();

        // Gerar todos os pares possíveis (sem repetidos e inversos)
        var paresPossiveis = new List<(MontagemCampeonatoModel, MontagemCampeonatoModel)>();
        for (int i = 0; i < listaMontagemCampeonato.Count; i++)
        {
            for (int j = i + 1; j < listaMontagemCampeonato.Count; j++)
            {
                paresPossiveis.Add((listaMontagemCampeonato[i], listaMontagemCampeonato[j]));
            }
        }

        MontagemCampeonatoModel ultimoJuiz = null;

        int maxRodadas = Campeonato.Campeonato_NumerosRodadas;

        for (int rodada = 0; rodada < maxRodadas; rodada++)
        {
            // Filtrar pares válidos (pares em que ambos times não ultrapassam jogos e ainda não jogaram entre si)
            var paresValidos = paresPossiveis
                .Where(p => p.Item1.Jogos < Campeonato.Campeonato_NumerosRodadas
                            && p.Item2.Jogos < Campeonato.Campeonato_NumerosRodadas
                            && !jogos.Any(j => (j.TimeCasaId == p.Item1.Id && j.TimeForaId == p.Item2.Id) || (j.TimeCasaId == p.Item2.Id && j.TimeForaId == p.Item1.Id)))
                .ToList();

            if (!paresValidos.Any())
            {
                // Não tem mais pares possíveis para formar jogo
                break;
            }

            // Escolher par que ajuda balancear os jogos
            (MontagemCampeonatoModel casa, MontagemCampeonatoModel fora) jogoEscolhido = (null, null);

            foreach (var par in paresValidos.OrderBy(p => p.Item1.Jogos + p.Item2.Jogos))
            {
                // Tentativa de juiz conforme regra do último juiz
                MontagemCampeonatoModel juizTentativo = ultimoJuiz ?? par.Item1;

                // Se juizTentativo ultrapassa limite, tentar o outro time
                if (juizTentativo.Juiz >= Campeonato.Campeonato_NumerosRodadas)
                {
                    var juizAlternativo = (juizTentativo == par.Item1) ? par.Item2 : par.Item1;
                    if (juizAlternativo.Juiz < Campeonato.Campeonato_NumerosRodadas)
                        juizTentativo = juizAlternativo;
                    else
                        continue; // Nenhum dos dois pode ser juiz, pular par
                }

                jogoEscolhido = par;
                break;
            }

            if (jogoEscolhido.casa == null)
            {
                // Nenhum par válido com juiz disponível, encerrar
                break;
            }

            // Definir juiz do jogo conforme regra
            MontagemCampeonatoModel juizJogo;
            if (ultimoJuiz == null)
                juizJogo = jogoEscolhido.casa;
            else if (jogoEscolhido.casa == ultimoJuiz || jogoEscolhido.fora == ultimoJuiz)
                juizJogo = ultimoJuiz;
            else
                juizJogo = (jogoEscolhido.casa.Juiz <= jogoEscolhido.fora.Juiz) ? jogoEscolhido.casa : jogoEscolhido.fora;

            // Criar e adicionar o jogo
            var jogo = new PartidaModel()
            {
                TimeCasaId = jogoEscolhido.casa.Id,
                TimeForaId = jogoEscolhido.fora.Id,
                TimeJuizId = juizJogo.Id
            };
            jogos.Add(jogo);

            var montagemCampeonatoRepository = new MontagemCampeonatoRepository();
            // Preencher as models times
            jogo.TimeCasa = listaMontagemCampeonato.Where(a => a.Id == jogo.TimeCasaId).Single();
            jogo.TimeFora = listaMontagemCampeonato.Where(a => a.Id == jogo.TimeForaId).Single();
            jogo.TimeJuiz = listaMontagemCampeonato.Where(a => a.Id == jogo.TimeJuizId).Single();

            // Atualizar contadores
            jogo.TimeCasa.Jogos++;
            jogo.TimeFora.Jogos++;
            jogo.TimeJuiz.Juiz++;

            // Atualizar último juiz para próxima rodada
            ultimoJuiz = jogo.TimeCasa;

            // Remover o par usado
            paresPossiveis.RemoveAll(p => (p.Item1 == jogo.TimeCasa && p.Item2 == jogo.TimeFora) || (p.Item1 == jogo.TimeFora && p.Item2 == jogo.TimeCasa));
        }

        // Validar se todos os times têm o mesmo número de jogos e juiz
        bool valido = listaMontagemCampeonato.All(t => t.Jogos == Campeonato.Campeonato_NumerosRodadas && t.Juiz == Campeonato.Campeonato_NumerosRodadas);

        if (!valido)
            throw new Exception("Não foi possível montar o campeonato com as regras dadas.");

        return jogos;
    }
    

    #endregion

    #region teste2
    public List<PartidaModel> MontarJogos(List<MontagemCampeonatoModel> listaMontagemCampeonato)
    {
        var random = new Random();
        var partidas = new List<PartidaModel>();
        var tentativas = 0;

        var numeroPartidas = (listaMontagemCampeonato.Count() * Campeonato.Campeonato_NumerosRodadas) / 2;

        while (partidas.Count < numeroPartidas && tentativas < 5000)
        {
            tentativas++;

            // Embaralhar times
            var times = listaMontagemCampeonato.OrderBy(x => random.Next()).ToList();

            var timeCasa = times[0];
            var timeFora = times[1];
            var timejuiz = times[2];

            // Regras de validação
            bool jaExiste = partidas.Any(p =>
                (p.TimeCasa == timeCasa && p.TimeFora == timeFora) ||
                (p.TimeCasa == timeFora && p.TimeFora == timeCasa));

            if (timeCasa != timeFora &&
                timejuiz != timeCasa &&
                timejuiz != timeFora &&
                !jaExiste)
            {
                partidas.Add(new PartidaModel
                {
                    Partida_Rodada = partidas.Count + 1,
                    TimeCasa = timeCasa,
                    TimeFora = timeFora,
                    TimeJuiz = timejuiz
                });
            }
        }

        return partidas;
    }

// rodada 1 = time2 x time1  juiz=time5
// rodada 2 = time4 x time3  juiz=time2
// rodada 3 = time1 x time5  juiz=time4
// rodada 4 = time3 x time2  juiz=time1
// rodada 5 = time5 x time4  juiz=time3
// rodada 6 = time1 x time3  juiz=time5
// rodada 7 = time2 x time4  juiz=time1
// rodada 8 = time3 x time5  juiz=time2
// rodada 9 = time4 x time1  juiz=time3
// rodada 10 = time5 x time2  juiz=time4








    

    #endregion
}