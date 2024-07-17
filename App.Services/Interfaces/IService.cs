using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Interfaces
{
    public interface IService
    {
        Task<List<TModel>> GetAsync();
        Task<T> GetByIdAsync(int id);
        
        void DeleteById(int id);
        void Add(T model);

        Task<T> UpdateAsync(int id, T model);
    }
}
