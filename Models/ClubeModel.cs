using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class ClubeModel: BaseModel
{
        [Key] // Chave primária
        public Guid Clube_Id { get; set; }
        public Image Clube_Logo { get; set; }
        public string Clube_Nome { get; set; }
        public List<JogadorModel> Lista_Jogadores { get; set; } = new();
}