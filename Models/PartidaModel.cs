using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class PartidaModel : BaseModel
{
    public DateTime Partida_DataHora { get; set; }
    public Guid FaseId { get; set; }
    public FaseModel Fase { get; set; }

    public Guid TimeCasaId { get; set; }
    public TimeModel TimeCasa { get; set; }

    public Guid TimeForaId { get; set; }
    public TimeModel TimeFora { get; set; }

    public int GolsCasa { get; set; }
    public int GolsFora { get; set; }
}