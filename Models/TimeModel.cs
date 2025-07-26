using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class TimeModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Time_Nome")]
    public string Time_Nome { get; set; }
    [Column("Time_GrupoId")]
    public Guid? Time_GrupoId { get; set; } // Pode ser null em fases mata-mata
    [Ignore]
    public virtual GrupoModel? GrupoModel { get; set; }
    [Ignore]
    public virtual List<JogadorModel> Lista_Jogadores { get; set; } = new();

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}