using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class JogadorModel
{
    [Key] // Chave primária
    public Guid Jogador_Id { get; set; }
    public string Jogador_Nome { get; set; }
    public string Jogador_Posicao { get; set; } // Ex: Goleiro, Zagueiro, etc.
    public Guid TimeId { get; set; }
    public TimeModel Time { get; set; }
}