namespace App.Api.Domain.Repositories;

public interface ITodoRepository : IRepository<Todo>
{
    IEnumerable<Todo> GetByUser(int userId);
}