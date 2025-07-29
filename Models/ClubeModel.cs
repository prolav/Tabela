using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class ClubeModel: BaseModel, INotifyPropertyChanged
{
        [MaxLength(100)][SQLite.Column("Clube_Nome")]
        public string Clube_Nome { get; set; }

        [SQLite.Column("Clube_Logo")]
        public string Clube_Logo { get; set; }  = "sem_imagem.jpeg";

        [SQLite.Column("Clube_Pais")] 
        public string Clube_Pais { get; set; } = "Brasil";
        [SQLite.Column("Clube_UF")]
        public string Clube_UF { get; set; } 
        [SQLite.Column("Clube_Cidade")]
        public string Clube_Cidade { get; set; } 
        [SQLite.Column("Clube_Bairro")]
        public string Clube_Bairro { get; set; }
        [SQLite.Column("Clube_Logradouro")]
        public string Clube_Logradouro { get; set; }
        [SQLite.Column("Clube_Presidente")]
        public string Clube_Presidente { get; set; } 
        [SQLite.Column("Clube_Telefone")]
        public string Clube_Telefone { get; set; } 
        [SQLite.Column("Clube_Email")]
        public string Clube_Email { get; set; }
        [ForeignKey ("FK_Regional_Id")][SQLite.Column("FK_Regional_Id")]
        public Guid FK_Regional_Id { get; set; }
        [Ignore]
        public virtual RegionalModel Regional { get; set; } = new RegionalModel();
        public virtual bool IsEven { get; set; } = false;
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
}