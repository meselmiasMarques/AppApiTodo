using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Domain.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        #region  Coleção

        public ICollection<Todo> Todos { get; set; }

        #endregion
    }
}
