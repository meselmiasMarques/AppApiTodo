using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T Update(T entity);
        void Delete(T entity);

        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
        
    }
}
