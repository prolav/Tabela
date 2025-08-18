using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Tabela.Models
{
    public class ClassificacaoModel : BaseModel, INotifyPropertyChanged
    {
        [ForeignKey("Classificacao_CampeonatoId")]
        [SQLite.Column("Classificacao_CampeonatoId")]
        public Guid Classificacao_CampeonatoId { get; set; }
        [ForeignKey("Classificacao_TimeId")]
        [SQLite.Column("Classificacao_TimeId")]
        public Guid Classificacao_TimeId { get; set; }
        [SQLite.Column("Classificacao_Vitoria")]
        public int Classificacao_Vitoria { get; set; }
        [SQLite.Column("Classificacao_Derrota")]
        public int Classificacao_Derrota { get; set; }
        [SQLite.Column("Classificacao_PontosPro")]
        public int Classificacao_PontosPro { get; set; }
        [SQLite.Column("Classificacao_PontosContra")]
        public int Classificacao_PontosContra { get; set; }
        [SQLite.Column("Classificacao_QtdeJogos")]
        public int Classificacao_QtdeJogos { get; set; }
        [Ignore]
        public virtual TimeModel Time { get; set; }
        [Ignore]
        public virtual int Classificacao_SaldoPontos => Classificacao_PontosPro - Classificacao_PontosContra;

        [Ignore]
        public virtual int Classificacao_Campo { get; set; } = 0;
        [Ignore]
        public virtual int Classificacao_Posicao { get; set; } = 0;
        public virtual bool IsEven { get; set; } = false;
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}