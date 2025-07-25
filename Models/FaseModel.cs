using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class FaseModel : BaseModel
{
    public string Fase_Nome { get; set; } // Ex: "Grupos", "Oitavas", "Final"
    public string Fase_Tipo { get; set; } // "Grupo" ou "MataMata"
    public Guid Fase_CampeonatoId { get; set; }
    public CampeonatoModel CampeonatoModel { get; set; }
    public List<GrupoModel> Lista_Grupos { get; set; } = new();
    public List<PartidaModel> Lista_Partidas { get; set; } = new();
}