using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class JogadorModel : BaseModel, INotifyPropertyChanged
{
    [ForeignKey("Jogador_ClubeId")]
    [SQLite.Column("Jogador_ClubeId")]
    public Guid Jogador_ClubeId { get; set; }

    [SQLite.Column("Jogador_Imagem")]
    public string Jogador_Imagem { get; set; }
    [SQLite.Column("Jogador_Nome")]
    public string Jogador_Nome { get; set; }
    [SQLite.Column("Jogador_Apelido")]
    public string Jogador_Apelido { get; set; }
    [Ignore]
    public virtual ClubeModel Clube { get; set; }
    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}