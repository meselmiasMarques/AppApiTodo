using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using App.Api.Domain.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Domain.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
            => _context = context;

        public void Add(Todo entity)
        {
            _context.Todos.Add(entity);
            _context.SaveChanges();
        }

        public Todo Update(Todo entity)
        {
            _context.Todos.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Todo entity)
        {
            _context.Todos.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }


        public Todo Get(int id)
            => _context.Todos.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Todo> GetAll()
            => _context.Todos.AsNoTracking().ToList();

        public IEnumerable<Todo> GetByUser(int userId)
            => _context.Todos
                .Where(u => u.UserId == userId)
                .ToList();
    }
}