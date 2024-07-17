using App.Api.Domain.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Api.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Domain.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
            => _context = context;

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public Category Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Category entity)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public Category Get(int id)
            => _context.Categories.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Category> GetAll()
            => _context.Categories
                .AsNoTracking()
                .ToList();

    }

}
