using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class UsuarioTipoModel
{
    [Key] // Chave primária
    public Guid UsuarioTipo_Id { get; set; }
    public string UsuarioTipo_Descricao { get; set; }
}