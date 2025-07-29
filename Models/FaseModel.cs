using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;


namespace Tabela.Models;

public class FaseModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Fase_Nome")]
    public string Fase_Nome { get; set; } // Ex: "Grupos", "Oitavas", "Final"

    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}