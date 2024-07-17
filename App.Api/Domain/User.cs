using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Domain.Domain
{
    public class User
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }
        
        #region Coleções
        
        public ICollection<Todo> Todos { get; set; }

        
        #endregion
    }
}
