using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Tabela.Models
{
    public class ClassificacaoModel : BaseModel, INotifyPropertyChanged
    {
        [PrimaryKey]
        public Guid Classificacao_PartidaId { get; set; }

        [PrimaryKey]
        public Guid Classificacao_TimeId { get; set; }

        [ForeignKey("Classificacao_CampeonatoId")]
        [SQLite.Column("Classificacao_CampeonatoId")]
        public Guid Classificacao_CampeonatoId { get; set; }

        [SQLite.Column("Classificacao_Vitoria")]
        public int Classificacao_Vitoria { get; set; } = 0;

        [SQLite.Column("Classificacao_Derrota")]
        public int Classificacao_Derrota { get; set; } = 0;

        [SQLite.Column("Classificacao_PontosPro")]
        public int Classificacao_PontosPro { get; set; } = 0;

        [SQLite.Column("Classificacao_PontosContra")]
        public int Classificacao_PontosContra { get; set; } = 0;

        public virtual int Classificacao_QtdeJogos { get; set; } = 0;

        [Ignore]
        public virtual TimeModel Time { get; set; }

        [Ignore]
        public virtual int Classificacao_SaldoPontos => Classificacao_PontosPro - Classificacao_PontosContra;

        public int Classificacao_Campo { get; set; } = 0;

        [Ignore]
        public virtual int Classificacao_Posicao { get; set; } = 0;

        public virtual bool IsEven { get; set; } = false;

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }


}
