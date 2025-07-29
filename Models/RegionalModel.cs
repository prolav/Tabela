using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class RegionalModel: BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Regional_Nome")]
    public string Regional_Nome { get; set; }
    [Column("Regional_Presidente")]
    public string Regional_Presidente { get; set; }
    [SQLite.Column("Regional_Telefone")]
    public string Regional_Telefone { get; set; } 
    [SQLite.Column("Regional_Email")]
    public string Regional_Email { get; set; }
    [Column("Regional_Dt_Inicio")]
    public DateTime Regional_Dt_Inicio { get; set; }
    [Column("Regional_Dt_Fim")]
    public DateTime Regional_Dt_Fim { get; set; }

    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}