using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class CampeonatoModel
{
    [Key] // Chave primária
    public Guid Campeonato_Id { get; set; }
    [Required] // Obrigatório
    [MaxLength(100)]
    public string Campeonato_Nome { get; set; }
    public DateTime Campeonato_Data { get; set; }
    public List<FaseModel> Campeonato_Fases { get; set; } = new();
}