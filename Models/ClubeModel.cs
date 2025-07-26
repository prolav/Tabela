using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class ClubeModel: BaseModel, INotifyPropertyChanged
{
        [MaxLength(100)][Column("Clube_Nome")]
        public string Clube_Nome { get; set; }

        [Column("Clube_Logo")]
        public Image Clube_Logo { get; set; }

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
}