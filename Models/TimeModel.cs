using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class TimeModel: BaseModel, INotifyPropertyChanged
{
    [ForeignKey ("FK_Campeonato_Id")][SQLite.Column("FK_Campeonato_Id")]
    public Guid FK_Campeonato_Id { get; set; }
    [ForeignKey ("FK_Clube_Id")][SQLite.Column("FK_Clube_Id")]
    public Guid FK_Clube_Id { get; set; }
    [SQLite.Column("MontagemCampeonatoModel_NumeroCampo")]
    public int MontagemCampeonatoModel_NumeroCampo { get; set; }
    [ForeignKey ("FK_Fase_Id")][SQLite.Column("FK_Fase_Id")]
    public Guid FK_Fase_Id { get; set; }

    [SQLite.Column("Apelido_Time")] 
    public string Apelido_Time
    {
        get => _apelido_Time;
        set
        {
            if (_apelido_Time == value) return;
                _apelido_Time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Apelido_Time)));
        }
    }
    private string _apelido_Time;
    [Ignore]
    public virtual int NumeroClubeNaqueleCampo { get; set; }
    [Ignore] 
    public virtual bool CampoHabilitado { get; set; } = false;
    [Ignore]
    public virtual List<ClubeModel> ListaClube { get; set; }

    private ClubeModel _clubeSelecionado;
    [Ignore]
    public virtual ClubeModel Clube
    {
        get => _clubeSelecionado;
        set
        {
            if (_clubeSelecionado != value)
            {
                _clubeSelecionado = value;
                //_clubeSelecionado = value = Clube.Clube_Nome=="Nenhum" ? null : value;
                OnPropertyChanged(nameof(Clube));

                // Atualiza o Apelido_Time quando o item selecionado mudar
                Apelido_Time = string.Empty;
            }
        }
    }





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