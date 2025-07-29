using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class JogadorModel : BaseModel, INotifyPropertyChanged
{
    [Column("Jogador_imagem")]
    public string Jogador_Imagem { get; set; }
    [MaxLength(100)][Column("Jogador_Nome")]
    public string Jogador_Nome { get; set; }
    [MaxLength(1)][Column("Jogador_Numero")]
    public int Jogador_Numero { get; set; } // Ex: Goleiro, Zagueiro, etc.

    [Ignore]
    public List<ClubeModel> Lista_Clubes { get; set; }

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}