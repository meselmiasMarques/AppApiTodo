namespace App.Api.Domain.Services;

public interface IUserService
{
    string Authenticate(string email, string password);
}