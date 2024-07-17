using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Api.Domain.Domain;
using App.Api.Domain.Repositories;
using App.Services.Interfaces;

namespace App.Services.Services
{
    public class ServiceCategory : IService
    {
        private readonly CategoryRepository _repository;
        public ServiceCategory(CategoryRepository repository)
            => _repository = repository;
        
        public Task<List<Category>> GetAsync()
            => _repository.GetAll();

        

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(T model)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int id, T model)
        {
            throw new NotImplementedException();
        }
    }
}
