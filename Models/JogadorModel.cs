using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class JogadorModel : BaseModel
{
    public Image Jogador_Imagem { get; set; }
    public string Jogador_Nome { get; set; }
    public string Jogador_Posicao { get; set; } // Ex: Goleiro, Zagueiro, etc.
    public List<ClubeModel> Lista_Clubes { get; set; }
}