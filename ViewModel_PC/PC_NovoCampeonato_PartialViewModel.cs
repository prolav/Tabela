using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_NovoCampeonato_PartialViewModel:BaseViewModel
{
    
    #region Fields
    private List<JogadorModel> _listaJogador;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<ClubeModel>  _listaClube;
    private int _numeroCampos;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel1;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel2;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel3;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel4;
    private List<MontagemCampeonatoModel> _listmontagemCampeonatoModel5;
    private CampeonatoModel _campeonatoModel;
    #endregion

    #region Properties
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel1 { get => _listmontagemCampeonatoModel1; set => SetProperty(ref _listmontagemCampeonatoModel1, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel2 { get => _listmontagemCampeonatoModel2; set => SetProperty(ref _listmontagemCampeonatoModel2, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel3 { get => _listmontagemCampeonatoModel3; set => SetProperty(ref _listmontagemCampeonatoModel3, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel4 { get => _listmontagemCampeonatoModel4; set => SetProperty(ref _listmontagemCampeonatoModel4, value); }
    public List<MontagemCampeonatoModel> ListmontagemCampeonatoModel5 { get => _listmontagemCampeonatoModel5; set => SetProperty(ref _listmontagemCampeonatoModel5, value); }
    public List<ClubeModel> ListaClube { get => _listaClube; set => SetProperty(ref _listaClube, value); }
    public CampeonatoModel CampeonatoModel { get => _campeonatoModel; set => SetProperty(ref _campeonatoModel, value); }
    
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    public int NumeroCampos
    {
        get => _numeroCampos;
        set
        {
            _numeroCampos = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(_campeonatoModel.Campeonato_NumerosCampos));
        }
    }
    #endregion
            
    #region Commands
    public ICommand AdicionarNovoCampeonatoCommand => new Command(() => AdicionarNovoCampeonatoExecute());
    #endregion
            
    #region Constructor
    public PC_NovoCampeonato_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM, CampeonatoModel campeonatoModel)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CampeonatoModel = campeonatoModel;

        CarregarDados();
        
    }
    #endregion

    #region Methods

    private void CarregarDados()
    {
        try
        {
            GeradorListaCampos();
            CarregarCorLista();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void AdicionarNovoCampeonatoExecute()
    {
        try
        {
            var x = 0;
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
            if (_campeonatoModel.Campeonato_NumerosCampos >= 1)
                CarregarCorCard(ListmontagemCampeonatoModel1);
            if (_campeonatoModel.Campeonato_NumerosCampos >= 2)
                CarregarCorCard(ListmontagemCampeonatoModel2);
            if (_campeonatoModel.Campeonato_NumerosCampos >= 3)
                CarregarCorCard(ListmontagemCampeonatoModel3);
            if (_campeonatoModel.Campeonato_NumerosCampos >= 4)
                CarregarCorCard(ListmontagemCampeonatoModel4);
            if (_campeonatoModel.Campeonato_NumerosCampos >= 5)
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
        var clubeRepository = new ClubeRepository();
        ListaClube = new List<ClubeModel>();
        var faseRepository = new FasesRepository();
        var faseId = faseRepository.GetAll().FirstOrDefault().Id;
        ListaClube = clubeRepository.GetAll();
        var montagemCampeonatoRepository = new MontagemCampeonatoRepository();
        ListmontagemCampeonatoModel1 = new List<MontagemCampeonatoModel>();
        ListmontagemCampeonatoModel2 = new List<MontagemCampeonatoModel>();
        ListmontagemCampeonatoModel3 = new List<MontagemCampeonatoModel>();
        ListmontagemCampeonatoModel4 = new List<MontagemCampeonatoModel>();
        ListmontagemCampeonatoModel5 = new List<MontagemCampeonatoModel>();

        if (_campeonatoModel.Campeonato_NumerosCampos >= 1)
        {
            for (int i = 1; i <= 6; i++)
            {
                var montagemCampeonato = new MontagemCampeonatoModel()
                {
                    FK_Campeonato_Id = _campeonatoModel.Id,
                    MontagemCampeonatoModel_NumeroCampo = 1,
                    FK_Fase_Id =faseId,
                    CampoHabilitado = true,
                    NumeroClube = i,
                    ListaClube = _listaClube
                };
                ListmontagemCampeonatoModel1.Add(montagemCampeonato);
            }
        }

        if (_campeonatoModel.Campeonato_NumerosCampos >= 2)
        {
            for (int i = 1; i <= 6; i++)
            {
                var montagemCampeonato = new MontagemCampeonatoModel()
                {
                    FK_Campeonato_Id = _campeonatoModel.Id,
                    MontagemCampeonatoModel_NumeroCampo = 2,
                    FK_Fase_Id =faseId,
                    CampoHabilitado = true,
                    NumeroClube = i,
                    ListaClube = _listaClube
                };
                ListmontagemCampeonatoModel2.Add(montagemCampeonato);
            }
        }

        if (_campeonatoModel.Campeonato_NumerosCampos >= 3)
        {
            for (int i = 1; i <= 6; i++)
            {
                var montagemCampeonato = new MontagemCampeonatoModel()
                {
                    FK_Campeonato_Id = _campeonatoModel.Id,
                    MontagemCampeonatoModel_NumeroCampo = 3,
                    FK_Fase_Id =faseId,
                    CampoHabilitado = true,
                    NumeroClube = i,
                    ListaClube = _listaClube
                };
                ListmontagemCampeonatoModel3.Add(montagemCampeonato);
            }
        }

        if (_campeonatoModel.Campeonato_NumerosCampos >= 4)
        {
            for (int i = 1; i <= 6; i++)
            {
                var montagemCampeonato = new MontagemCampeonatoModel()
                {
                    FK_Campeonato_Id = _campeonatoModel.Id,
                    MontagemCampeonatoModel_NumeroCampo = 4,
                    FK_Fase_Id =faseId,
                    CampoHabilitado = true,
                    NumeroClube = i,
                    ListaClube = _listaClube
                };
                ListmontagemCampeonatoModel4.Add(montagemCampeonato);
            }
        }

        if (_campeonatoModel.Campeonato_NumerosCampos >= 5)
        {
            for (int i = 1; i <= 6; i++)
            {
                var montagemCampeonato = new MontagemCampeonatoModel()
                {
                    FK_Campeonato_Id = _campeonatoModel.Id,
                    MontagemCampeonatoModel_NumeroCampo = 5,
                    FK_Fase_Id =faseId,
                    CampoHabilitado = true,
                    NumeroClube = i,
                    ListaClube = _listaClube
                };
                ListmontagemCampeonatoModel5.Add(montagemCampeonato);
            }
        }
    }
    #endregion

    #region Helpers
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    #endregion
}