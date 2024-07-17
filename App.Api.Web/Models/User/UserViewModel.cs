using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Api.Web.Models.User;

public class UserViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
    public string Name { get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [MinLength(3)]
    [MaxLength(100)]
    public string Role { get; set; }

    public string Email { get; set; }
}