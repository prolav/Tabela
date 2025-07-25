using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public abstract class BaseModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Criacao_Data { get; set; }
    public DateTime Altecacao_Data { get; set; }
    public bool Ativo { get; set; } = true;
}