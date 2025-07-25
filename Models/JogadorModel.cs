using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class JogadorModel : BaseModel
{
    [Key] // Chave primária
    public Guid Jogador_Id { get; set; }
    public string Jogador_Nome { get; set; }
    public Image Jogador_Imagem { get; set; }
    public string Jogador_Posicao { get; set; } // Ex: Goleiro, Zagueiro, etc.
    public Guid TimeId { get; set; }
    public List<TimeModel> Lista_Times { get; set; }
}