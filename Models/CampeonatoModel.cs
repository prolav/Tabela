using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace Tabela.Models;

public class CampeonatoModel : BaseModel, INotifyPropertyChanged
{
    [MaxLength(100)][Column("Campeonato_Nome")]
    public string Campeonato_Nome { get; set; }
    [Column("Campeonato_Data")]
    public DateTime Campeonato_Data { get; set; }
    [Column("Campeonato_NumerosCampos")]
    public int Campeonato_NumerosCampos { get; set; }
    [Column("Campeonato_NumerosRodadas")]
    public int Campeonato_NumerosRodadas { get; set; }
    [Column("Campeonato_Local")]
    public string Campeonato_Local { get; set; }
    private bool _campeonato_Cadastrado;
    [Column("Campeonato_Cadastrado")] 
    public bool Campeonato_Cadastrado
    {
        get => _campeonato_Cadastrado;
        set
        {
            if (_campeonato_Cadastrado != value)
            {
                _campeonato_Cadastrado = value;
                OnPropertyChanged();
            }
        }
    }
    private bool _campeonato_HistoricoJogos_Cadastrado;
    [Column("Campeonato_HistoricoJogos_Cadastrado")] 
    public bool Campeonato_HistoricoJogos_Cadastrado
    {
        get => _campeonato_HistoricoJogos_Cadastrado;
        set
        {
            if (_campeonato_HistoricoJogos_Cadastrado != value)
            {
                _campeonato_HistoricoJogos_Cadastrado = value;
                OnPropertyChanged();
            }
        }
    }


    [Ignore]
    public virtual List<FaseModel> Campeonato_Fases { get; set; } = new();

    #region Notify
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion


}