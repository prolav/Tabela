using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class CampoModel : BaseModel, INotifyPropertyChanged
{
    [Column("NomeCampo")]
    public string NomeCampo { get; set; }

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}