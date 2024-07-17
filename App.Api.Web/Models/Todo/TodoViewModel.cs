using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Api.Web.Models.Todo
{
    public class TodoViewModel
    {
        [Required(ErrorMessage = "Campo Título é obrigatório")]
        [MaxLength(100,ErrorMessage = "Maximo 100 caracteres")]
        [MinLength(3,ErrorMessage = "Minimo 3 caracteres")]
        public string? Title { get; set; }

        [MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Campo Categoria é obrigatório")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Campo Usuário é obrigatório")]
        public int UserId { get; set; }

       
    }
}
