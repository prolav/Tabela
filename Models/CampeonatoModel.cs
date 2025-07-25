using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class CampeonatoModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Campeonato_Nome")]
    public string Campeonato_Nome { get; set; }
    [Column("Campeonato_Data")]
    public DateTime Campeonato_Data { get; set; }

    [Ignore]
    public virtual List<FaseModel> Campeonato_Fases { get; set; } = new();

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}