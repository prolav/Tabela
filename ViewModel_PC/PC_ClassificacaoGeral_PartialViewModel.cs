using System.Windows.Input;
using Tabela.Models;
using Tabela.Repositories;
using Tabela.Repositories.Repositories;

namespace Tabela.ViewModel_PC;

public class PC_ClassificacaoGeral_PartialViewModel:BaseViewModel
{
    
    #region Fields
    // private string _nomeUsuario;
    private CampeonatoModel _campeonato;
    private PC_DashBoardViewModel _pc_DashBoardVM;
    private List<ClassificacaoModel> _listaClassificacaoCampo1;
    private List<ClassificacaoModel> _listaClassificacaoCampo2;
    private List<ClassificacaoModel> _listaClassificacaoCampo3;
    private List<ClassificacaoModel> _listaClassificacaoCampo4;
    private List<ClassificacaoModel> _listaClassificacaoCampo5;
    #endregion

    #region Properties
    public CampeonatoModel Campeonato { get => _campeonato; set => SetProperty(ref _campeonato, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo1 { get => _listaClassificacaoCampo1; set => SetProperty(ref _listaClassificacaoCampo1, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo2 { get => _listaClassificacaoCampo2; set => SetProperty(ref _listaClassificacaoCampo2, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo3 { get => _listaClassificacaoCampo3; set => SetProperty(ref _listaClassificacaoCampo3, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo4 { get => _listaClassificacaoCampo4; set => SetProperty(ref _listaClassificacaoCampo4, value); }
    public List<ClassificacaoModel> ListaClassificacaoCampo5 { get => _listaClassificacaoCampo5; set => SetProperty(ref _listaClassificacaoCampo5, value); }
    //
    // public string Senha { get => _senha; set => SetProperty(ref _senha, value); }
    #endregion

    #region Commands

    #endregion
    
    #region Constructor
    public PC_ClassificacaoGeral_PartialViewModel(PC_DashBoardViewModel pc_DashBoardVM)
    {
        _pc_DashBoardVM = pc_DashBoardVM;
        CarregarDados();
    }
    #endregion

    #region Methods

    private void CarregarDados()
    {
        try
        {
            var campeonatoRepository = new CampeonatoRepository();
            Campeonato = campeonatoRepository.GetAll().LastOrDefault();
            CarregaListaCampos();
            CarregarCadastrarCamposNulosListaClassificacao();
            OrdenarListaClassificacao();
            CarregarCadastrarPosicoes();
        }
        catch (Exception e)
        {
            Application.Current.MainPage.DisplayAlert("Erro", e.Message, "OK");
        }
    }

    private void CarregarCadastrarPosicoes()
    {
        try
        {
            if (Campeonato.Campeonato_NumerosCampos >= 1)
                CadastrarPosicoesListaClassificacao(ListaClassificacaoCampo1);
            if (Campeonato.Campeonato_NumerosCampos >= 2)
                CadastrarPosicoesListaClassificacao(ListaClassificacaoCampo2);
            if (Campeonato.Campeonato_NumerosCampos >= 3)
                CadastrarPosicoesListaClassificacao(ListaClassificacaoCampo3);
            if (Campeonato.Campeonato_NumerosCampos >= 4)
                CadastrarPosicoesListaClassificacao(ListaClassificacaoCampo4);
            if (Campeonato.Campeonato_NumerosCampos == 5)
                CadastrarPosicoesListaClassificacao(ListaClassificacaoCampo5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CadastrarPosicoesListaClassificacao(List<ClassificacaoModel> listaClassificacao)
    {
        try
        {
            for (int i = 0; i < listaClassificacao.Count; i++)
            {
                listaClassificacao[i].Classificacao_Posicao = i + 1;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CarregarCadastrarCamposNulosListaClassificacao()
    {
        try
        {
            if (Campeonato.Campeonato_NumerosCampos >= 1)
                CadastrarCamposNulosListaClassificacao(ListaClassificacaoCampo1);
            if (Campeonato.Campeonato_NumerosCampos >= 2)
                CadastrarCamposNulosListaClassificacao(ListaClassificacaoCampo2);
            if (Campeonato.Campeonato_NumerosCampos >= 3)
                CadastrarCamposNulosListaClassificacao(ListaClassificacaoCampo3);
            if (Campeonato.Campeonato_NumerosCampos >= 4)
                CadastrarCamposNulosListaClassificacao(ListaClassificacaoCampo4);
            if (Campeonato.Campeonato_NumerosCampos == 5)
                CadastrarCamposNulosListaClassificacao(ListaClassificacaoCampo5);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void OrdenarListaClassificacao()
    {
        try
        {
            if (Campeonato.Campeonato_NumerosCampos >= 1)
                ListaClassificacaoCampo1 = ListaClassificacaoCampo2.OrderByDescending(x => x.Classificacao_Vitoria).ThenByDescending(x => x.Classificacao_SaldoPontos).ThenByDescending(x => x.Classificacao_PontosPro).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 2)
                ListaClassificacaoCampo2 = ListaClassificacaoCampo2.OrderByDescending(x => x.Classificacao_Vitoria).ThenByDescending(x => x.Classificacao_SaldoPontos).ThenByDescending(x => x.Classificacao_PontosPro).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 3)
                ListaClassificacaoCampo3 = ListaClassificacaoCampo3.OrderByDescending(x => x.Classificacao_Vitoria).ThenByDescending(x => x.Classificacao_SaldoPontos).ThenByDescending(x => x.Classificacao_PontosPro).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 4)
                ListaClassificacaoCampo4 = ListaClassificacaoCampo4.OrderByDescending(x => x.Classificacao_Vitoria).ThenByDescending(x => x.Classificacao_SaldoPontos).ThenByDescending(x => x.Classificacao_PontosPro).ToList();
            if (Campeonato.Campeonato_NumerosCampos == 5)
                ListaClassificacaoCampo5 = ListaClassificacaoCampo5.OrderByDescending(x => x.Classificacao_Vitoria).ThenByDescending(x => x.Classificacao_SaldoPontos).ThenByDescending(x => x.Classificacao_PontosPro).ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void CadastrarCamposNulosListaClassificacao(List<ClassificacaoModel> listaClassificacao)
    {
        try
        {
            var timeRepository = new TimeRepository();
            foreach (var classificacao in listaClassificacao)
            {
                classificacao.Time = new TimeModel();
                classificacao.Time = timeRepository.GetAll().FirstOrDefault(a => a.Id == classificacao.Classificacao_TimeId);              
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    private void CarregaListaCampos()
    {
        try
        {
            var classificacaoRepository = new ClassificacaoRepository();
            ListaClassificacaoCampo1 = classificacaoRepository.GetAll().Where(a => a.Classificacao_Campo == 1 && a.Classificacao_CampeonatoId == Campeonato.Id).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 2)
                ListaClassificacaoCampo2 = classificacaoRepository.GetAll().Where(a => a.Classificacao_Campo == 2 && a.Classificacao_CampeonatoId == Campeonato.Id).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 3)
                ListaClassificacaoCampo3 = classificacaoRepository.GetAll().Where(a => a.Classificacao_Campo == 3 && a.Classificacao_CampeonatoId == Campeonato.Id).ToList();
            if (Campeonato.Campeonato_NumerosCampos >= 4)
                ListaClassificacaoCampo4 = classificacaoRepository.GetAll().Where(a => a.Classificacao_Campo == 4 && a.Classificacao_CampeonatoId == Campeonato.Id).ToList();
            if (Campeonato.Campeonato_NumerosCampos == 5)
                ListaClassificacaoCampo5 = classificacaoRepository.GetAll().Where(a => a.Classificacao_Campo == 5 && a.Classificacao_CampeonatoId == Campeonato.Id).ToList();     
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    #endregion
}