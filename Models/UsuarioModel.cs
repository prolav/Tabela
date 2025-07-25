using System.ComponentModel.DataAnnotations;

namespace Tabela.Models;

public class UsuarioModel : BaseModel
{
    [Key] // Chave primária
    public Guid Usuario_Id { get; set; }
    public string Usuario_Login { get; set; }
    public string Usuario_Password { get; set; }
    public UsuarioTipoModel UsuarioTipoModel { get; set; }
}