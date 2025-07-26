using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class UsuarioModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Usuario_Login")]
    public string Usuario_Login { get; set; }
    [MaxLength(100)][Column("Usuario_Password")]
    public string Usuario_Password { get; set; }
    [MaxLength(100)][Column("Usuario_Tipo")]
    public string Usuario_Tipo { get; set; }

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
