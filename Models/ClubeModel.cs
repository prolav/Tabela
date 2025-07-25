using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class ClubeModel: BaseModel
{
        public Image Clube_Logo { get; set; }
        public string Clube_Nome { get; set; }
}