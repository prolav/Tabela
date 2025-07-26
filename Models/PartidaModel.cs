using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class PartidaModel : BaseModel, INotifyPropertyChanged
{
    [Column("Partida_DataHora")]
    public DateTime Partida_DataHora { get; set; }
    [Column("FaseId")]
    public Guid FaseId { get; set; }
    [Ignore]
    public virtual FaseModel Fase { get; set; }
    [Column("TimeCasaId")]
    public Guid TimeCasaId { get; set; }
    [Ignore]
    public virtual TimeModel TimeCasa { get; set; }
    [Column("TimeForaId")]
    public Guid TimeForaId { get; set; }
    [Ignore]
    public virtual TimeModel TimeFora { get; set; }
    [Column("PontosCasa")]
    public int PontosCasa { get; set; }
    [Column("PontosFora")]
    public int PontosFora { get; set; }

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}