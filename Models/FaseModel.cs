using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;


namespace Tabela.Models;

public class FaseModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Fase_Nome")]
    public string Fase_Nome { get; set; } // Ex: "Grupos", "Oitavas", "Final"
    [MaxLength(100)][Column("Fase_Tipo")]
    public string Fase_Tipo { get; set; } // "Grupo" ou "MataMata"
    [Column("Fase_CampeonatoId")]
    public Guid Fase_CampeonatoId { get; set; }


    [Ignore]
    public virtual CampeonatoModel CampeonatoModel { get; set; }
    [Ignore]
    public virtual List<GrupoModel> Lista_Grupos { get; set; } = new();
    [Ignore]
    public virtual List<PartidaModel> Lista_Partidas { get; set; } = new();

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}