using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class GrupoModel : BaseModel
{
    public string Grupo_Nome { get; set; } // Ex: "A", "B", etc.
    public Guid Grupo_FaseId { get; set; }
    public FaseModel FaseModel { get; set; }
    public List<TimeModel> TimesModel { get; set; } = new();
}