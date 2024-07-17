using App.Api.Domain.Domain;
using App.Api.Domain.Domain.Data;
using App.Api.Domain.Repositories;
using App.Api.Domain.Responses.ApiResponses;
using App.Api.Domain.Services;
using App.Api.Web.Models.Login;
using App.Api.Web.Models.User;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Web.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AppDbContext _context;

    public AuthController(IUserService userService, AppDbContext context)
    {
        _userService = userService;
        _context = context;

    }

    [HttpPost("v1/Auth/")]
    public IActionResult Register(UserViewModel model)
    {
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Password = model.Password,
            Role = model.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new ApiResponse<User>
        {
            Success = true,
            Message = "Usuário registrado!"
        });
    }

    [HttpPost("v1/Login")]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new ApiResponse<IEnumerable<string>>
            {
                Success = false,
                Message = "Revise os campos",
                Errors = errors
            });
        }
        
        var token = _userService.Authenticate(model.Email, model.Password);
        if (token == null)
        {
            return Unauthorized(new { Message = "Credenciais inválidas" });
        }

        return Ok(new { Token = token });
    }
    
    
}