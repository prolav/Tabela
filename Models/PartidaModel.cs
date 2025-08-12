using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class PartidaModel : BaseModel, INotifyPropertyChanged
{
    [ForeignKey ("FK_Campeonato_Id")][SQLite.Column("FK_Campeonato_Id")]
    public Guid FK_Campeonato_Id { get; set; }
    [SQLite.Column("Partida_DataHora")]
    public DateTime Partida_DataHora { get; set; }
    [SQLite.Column("Partida_Rodada")]
    public int Partida_Rodada { get; set; }
    [SQLite.Column("Partida_NumeroCampo")]
    public int Partida_NumeroCampo { get; set; }
    [ForeignKey ("FaseId")][SQLite.Column("FaseId")]
    public Guid FaseId { get; set; }
    [Ignore]
    public FaseModel Fase { get; set; }
    [ForeignKey ("TimeCasaId")][SQLite.Column("TimeCasaId")]
    public Guid TimeCasaId { get; set; }
    [Ignore]
    public virtual MontagemCampeonatoModel TimeCasa { get; set; }
    [ForeignKey ("TimeForaId")][SQLite.Column("TimeForaId")]
    public Guid TimeForaId { get; set; }
    [ForeignKey ("TimeJuizId")][SQLite.Column("TimeJuizId")]
    public Guid TimeJuizId { get; set; }
    [Ignore]
    public virtual MontagemCampeonatoModel TimeJuiz { get; set; }
    [Ignore]
    public virtual MontagemCampeonatoModel TimeFora { get; set; }
    [SQLite.Column("Partida_PontosCasa")]
    public int PontosCasa { get; set; }
    [SQLite.Column("Partida_PontosFora")]
    public int PontosFora { get; set; }
    [SQLite.Column("Partida_ComJogo")] 
    public bool Partida_ComJogo { get; set; } = false;
    
    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}