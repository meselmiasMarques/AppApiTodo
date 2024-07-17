using App.Api.Domain.Domain;
using App.Api.Domain.Repositories;
using App.Api.Domain.Responses.ApiResponses;
using App.Api.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Web.Controllers;

[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _repository;

    public UserController(IRepository<User> repository)
        => _repository = repository;


    [HttpGet("v1/users/")]
    public IActionResult GetAll()
    {
        try
        {
            var users = _repository.GetAll();

            if (users == null)
            {
                return NotFound(new ApiResponse<User>
                {
                    Success = false,
                    Message = "Não foi encontrado !"
                });
            }
            
            return Ok(new ApiResponse<IEnumerable<User>>
            {
                Success = true,
                Message = "Lista de Usuários",
                Data = users
            });
        }
        catch (DbUpdateException e)
        {
            return BadRequest(new ApiResponse<User>
            {
                Success = false,
                Errors = e.Data,
                Message = e.Message
            });
        }
    }

    [HttpPost("v1/users/")]
    public IActionResult Post(UserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new ApiResponse<IEnumerable<string>>
            {
                Success = false,
                Message = "Erros nas validações.",
                Errors = errors
            });
        }

        try
        {

            var user = new User
            {
                Id = 0,
                Name = model.Name,
                Password = model.Password,
                Role = model.Role
            };
        
            _repository.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new ApiResponse<User>
            {
                Success = true,
                Message = "Todo created successfully.",
                Data = user
            });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpGet("v1/users/{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = _repository.Get(id);
        if (user == null)
        {
            return BadRequest(new ApiResponse<User>
            {
                Success = false,
                Message = "Não foi possível recuperar o usuario"
            });
        }

        return Ok(new ApiResponse<User>
        {
            Success = true,
            Data = user
        });
    }

    [HttpPut("v1/users/{id:int}")]
    public IActionResult Put(int id, [FromBody] UserViewModel model)
    {
        try
        {
            var user = _repository.Get(id);
            if (user == null)
            {
                return BadRequest(new ApiResponse<User>
                {

                    Success = false,
                    Message = "Erro ao recuperar usuário"
                });
            }

            user.Name = model.Name;
            user.Password = model.Password;
            user.Role = model.Role;

            _repository.Update(user);

            return Ok(new ApiResponse<User>
            {
                Success = true,
                Data = user,
                Message = "Usuário Alterado!"
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse<IEnumerable<User>>
            {
                Success = true,
                Message = e.Message,
                Errors = e.Data,
                
            });
        }
         
    }
    [HttpDelete("v1/users/{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var user = _repository.Get(id);
            if (user == null)
            {
                return BadRequest(new ApiResponse<User>
                {

                    Success = false,
                    Message = "Erro ao recuperar usuário"
                });
            }
            
            _repository.Delete(user);

            return Ok(new ApiResponse<User>
            {
                Success = true,
                Data = user,
                Message = "Usuário Deletado!"
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse<IEnumerable<User>>
            {
                Success = true,
                Message = e.Message,
                Errors = e.Data,
                
            });
        }
         
    }
    
}