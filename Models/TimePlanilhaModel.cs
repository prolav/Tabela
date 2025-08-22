using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class TimePlanilhaModel: BaseModel, INotifyPropertyChanged
{
    [ForeignKey ("FK_Time_Id")][SQLite.Column("FK_Time_Id")]
    public Guid FK_Jogador1_Id { get; set; }
    public Guid FK_Jogador2_Id { get; set; }
    public Guid FK_Jogador3_Id { get; set; }
    public Guid FK_Jogador4_Id { get; set; }
    public Guid FK_jogador5_Id { get; set; }

    [Ignore]
    public virtual JogadorModel Jogador1 { get; set; }
    [Ignore]
    public virtual JogadorModel Jogador2 { get; set; }
    [Ignore]
    public virtual JogadorModel Jogador3 { get; set; }
    [Ignore]
    public virtual JogadorModel Jogador4 { get; set; }
    [Ignore]
    public virtual JogadorModel Jogador5 { get; set; }

    public virtual bool IsEven { get; set; } = false;
    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion

    #region 
    [Ignore]
    public virtual int Jogos { get; set; }
    [Ignore]
    public virtual int Juiz { get; set; }
    #endregion
}