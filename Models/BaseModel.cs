namespace Tabela.Models;

public abstract class BaseModel
{
    public DateTime Criacao_Data { get; set; }
    public DateTime Altecacao_Data { get; set; }
    public bool Ativo { get; set; } = true;
}