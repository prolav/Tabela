using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class TimeModel : BaseModel
{
    [Key] // Chave primária
    public Guid Time_Id { get; set; }
    public string Time_Nome { get; set; }
    public Guid? Time_GrupoId { get; set; } // Pode ser null em fases mata-mata
    public GrupoModel? GrupoModel { get; set; }
    public List<JogadorModel> Lista_Jogadores { get; set; } = new();
}