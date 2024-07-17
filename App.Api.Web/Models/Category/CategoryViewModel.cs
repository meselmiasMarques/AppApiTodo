using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Api.Web.Models.Category
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é requerido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 usuários")]
        [MinLength(3, ErrorMessage = "Minimo 3 Caracteres")]
        public string Name { get; set; }
    }
}
