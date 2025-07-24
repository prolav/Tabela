using System.Windows.Input;

namespace Tabela.ViewModels;

public class PrimeiroViewModel
{
    #region Fields
    private string _titulo;
    private bool _estaCarregando;
    #endregion

    #region Properties
    public string Titulo { get => _titulo; set => SetProperty(ref _titulo, value); }
    public bool EstaCarregando { get => _estaCarregando; set => SetProperty(ref _estaCarregando, value); }
    #endregion

    #region Commands
    public ICommand CarregarCommand { get; }
    #endregion

    #region Constructor
    public PrimeiroViewModel() { CarregarCommand = new Command(ExecutarCarregamento); }
    #endregion

    #region Methods
    private void ExecutarCarregamento()
    {
        EstaCarregando = true;
        // Simulação de carregamento
        Task.Delay(1000).ContinueWith(_ =>
        {
            EstaCarregando = false;
            Titulo = "Dados carregados";
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    #endregion
}