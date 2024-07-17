using System.ComponentModel.DataAnnotations;

namespace App.Api.Web.Models.Login;

public class LoginViewModel
{
    [Required(ErrorMessage = "O Campo Email é obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo Senha é requerido")]
    [MinLength(4, ErrorMessage = "Mínimo 4 carateres")]
    public string Password { get; set; }
}