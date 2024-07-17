using App.Api.Domain.Domain;
using App.Api.Domain.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Domain.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();
    }

    public User Update(User entity)
    {
        _context.Users.Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(User entity)
    {
        _context.Users.Remove(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public User Get(int id)
        => _context.Users.FirstOrDefault(x => x.Id == id);

    public IEnumerable<User> GetAll()
        => _context.Users.AsNoTracking().ToList();
}