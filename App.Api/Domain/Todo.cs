using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Api.Domain.Domain;
using App.Api.Domain.enums;

namespace App.Api.Domain
{
    public class Todo
    {
        #region ATRIBUTOS
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        #endregion

        #region RELACIONAMENTOS
        public Category Category { get; set; }
        
        
        public User User { get; set; }

        

        #endregion
    }
}
