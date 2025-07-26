using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class GrupoModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Grupo_Nome")]
    public string Grupo_Nome { get; set; } // Ex: "A", "B", etc.
    [Column("Grupo_FaseId")]
    public Guid Grupo_FaseId { get; set; }

    // aparentemente falta a tabela grupo fase time
    [Ignore]
    public virtual FaseModel FaseModel { get; set; }
    [Ignore]
    public virtual List<TimeModel> TimesModel { get; set; } = new();

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}