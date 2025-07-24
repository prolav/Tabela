using System.Windows.Input;

namespace Tabela.ViewModels_Celular;    
public class TopoTemplateViewModel : BaseViewModel
{
    #region Fields
    private string _titulo;
    private ImageSource _imagem;

    #endregion

    #region Properties
    public string Titulo { get => _titulo; set => SetProperty(ref _titulo, value); }

    public ImageSource Imagem { get => _imagem; set => SetProperty(ref _imagem, value); }
    #endregion
    
    #region Commands
    public ICommand ImagemClicadaCommand { get; }
    #endregion
    
    #region Constructor
    public TopoTemplateViewModel()
    {
        Titulo = "Bem-vindo!";
        Imagem = ImageSource.FromFile("topo_default.png"); // ou FromUri, FromStream etc.
        ImagemClicadaCommand = new Command(ExecutarImagemClicada);
    }
    #endregion

    #region Methods
    private void ExecutarImagemClicada()
    {
        // Aqui você pode colocar navegação, exibir mensagem, etc.
        Console.WriteLine("Imagem clicada!");
    }
    #endregion
}

