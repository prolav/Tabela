using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class MontagemCampeonatoModel: BaseModel, INotifyPropertyChanged
{
    [ForeignKey ("FK_Campeonato_Id")][SQLite.Column("FK_Campeonato_Id")]
    public Guid FK_Campeonato_Id { get; set; }
    [ForeignKey ("FK_Clube_Id")][SQLite.Column("FK_Clube_Id")]
    public int FK_Clube_Id { get; set; }
    [SQLite.Column("MontagemCampeonatoModel_NumeroCampo")]
    public int MontagemCampeonatoModel_NumeroCampo { get; set; }
    [ForeignKey ("FK_Fase_Id")][SQLite.Column("FK_Fase_Id")]
    public Guid FK_Fase_Id { get; set; }

    [SQLite.Column("Apelido_Time")]
    public string Apelido_Time { get; set; }

    [Ignore]
    public virtual int NumeroClube { get; set; }
    [Ignore] 
    public virtual bool CampoHabilitado { get; set; } = false;
    [Ignore]
    public virtual ClubeModel Clube { get; set; } = new ClubeModel();
    [Ignore]
    public virtual List<ClubeModel> ListaClube { get; set; } = new List<ClubeModel>(); 
    [Ignore]
    public virtual RegionalModel Regional { get; set; } = new RegionalModel();
    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}