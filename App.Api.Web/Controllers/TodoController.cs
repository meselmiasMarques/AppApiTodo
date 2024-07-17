using App.Api.Domain;
using App.Api.Domain.enums;
using App.Api.Domain.Repositories;
using App.Api.Domain.Responses.ApiResponses;
using App.Api.Web.Models.Todo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.Api.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
            => _repository = repository;

        [HttpGet("v1/todos")]
        public IActionResult Get()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (User == null)
                {
                    return Unauthorized(new ApiResponse<IEnumerable<Todo>>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todos = _repository.GetByUser(userId);

                return Ok(new ApiResponse<IEnumerable<Todo>>
                {
                    Success = true,
                    Message = "Lista de Todos",
                    Data = todos
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<IEnumerable<Todo>>
                {
                    Success = false,
                    Message = e.Message,
                    Errors = e.Data
                });
            }
        }

        [HttpGet("v1/todos/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todo = _repository.Get(id);
                if (todo == null || todo.UserId != userId)
                {
                    return NotFound(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Todo não encontrado"
                    });
                }

                return Ok(new ApiResponse<Todo>
                {
                    Success = true,
                    Data = todo
                });
            }
            catch (DbUpdateException e)
            {
                return BadRequest(new ApiResponse<Todo>
                {
                    Success = false,
                    Data = null,
                    Errors = e.Data,
                    Message = "Erro ao recuperar Todo"
                });
            }
        }

        [HttpPut("v1/todos/{id:int}")]
        public IActionResult Put(int id, TodoViewModel model)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todo = _repository.Get(id);
                if (todo == null || todo.UserId != userId)
                {
                    return NotFound(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Todo não encontrado"
                    });
                }

                todo.Title = model.Title;
                todo.Description = model.Description;
                todo.CategoryId = model.CategoryId;

                _repository.Update(todo);
                return Ok(new ApiResponse<Todo>
                {
                    Success = true,
                    Data = todo,
                    Message = "Todo atualizado"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<Todo>
                {
                    Success = false,
                    Message = e.Message,
                    Errors = e.Data
                });
            }
        }

        [HttpPost("v1/todos")]
        public IActionResult Post(TodoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Valide os dados novamente"
                    });
                }

                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todo = new Todo
                {
                    Title = model.Title,
                    Description = model.Description,
                    Status = Status.Pending,
                    CategoryId = model.CategoryId,
                    UserId = userId
                };

                _repository.Add(todo);
                return Ok(new ApiResponse<Todo>
                {
                    Success = true,
                    Data = todo,
                    Message = "Todo criado com sucesso"
                });
            }
            catch (DbUpdateException e)
            {
                return BadRequest(new ApiResponse<Todo>
                {
                    Success = false,
                    Message = e.Message,
                    Errors = e.Data
                });
            }
        }

        [HttpDelete("v1/todos/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todo = _repository.Get(id);
                if (todo == null || todo.UserId != userId)
                {
                    return NotFound(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Todo não encontrado"
                    });
                }

                _repository.Delete(todo);
                return Ok(new ApiResponse<Todo>
                {
                    Success = true,
                    Data = todo,
                    Message = "Todo excluído com sucesso"
                });
            }
            catch (DbUpdateException e)
            {
                return BadRequest(new ApiResponse<Todo>
                {
                    Success = false,
                    Errors = e.Data,
                    Message = "Erro ao excluir todo"
                });
            }
        }

        [HttpGet("v1/todosUser")]
        public IActionResult GetTodoByUser()
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userId == null)
                {
                    return Unauthorized(new ApiResponse<Todo>
                    {
                        Success = false,
                        Message = "Usuário não autenticado"
                    });
                }

                var todos = _repository.GetByUser(userId);

                return Ok(new ApiResponse<IEnumerable<Todo>>
                {
                    Success = true,
                    Data = todos,
                    Message = "Lista Todo por usuário"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<Todo>
                {
                    Success = false,
                    Errors = e.Data,
                    Message = e.Message
                });
            }
        }
    }
}




//using App.Api.Domain;
//using App.Api.Domain.enums;
//using App.Api.Domain.Repositories;
//using App.Api.Domain.Responses.ApiResponses;
//using App.Api.Web.Models.Todo;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace App.Api.Web.Controllers
//{
//    [ApiController]
//    [Authorize]
//    public class TodoController : ControllerBase
//    {
//        private readonly ITodoRepository _repository;

//        public TodoController(ITodoRepository repository)
//            => _repository = repository;

//        [HttpGet("v1/todos/")]
//        public IActionResult Get()
//        {

//            try
//            {

//                var todos = _repository.GetAll();

//                return Ok(new ApiResponse<IEnumerable<Todo>>
//                {
//                    Success = true,
//                    Message = "Lista de Todos",
//                    Data = todos
//                });
//            }
//            catch (Exception e)
//            {
//                return BadRequest(new ApiResponse<IEnumerable<Todo>>
//                {
//                    Success = false,
//                    Message = e.Message,
//                    Errors = e.Data
//                });
//            }

//        }

//        [HttpGet("v1/todos/{id:int}")]
//        public IActionResult GetById(int id)
//        {
//            try
//            {
//                var todo = _repository.Get(id);
//                if (todo == null)
//                {
//                    return NotFound(new ApiResponse<Todo>
//                    {
//                        Success = false,
//                        Message = "Todo não Encontrado! ",
//                        Data = null

//                    });
//                }

//                return Ok(new ApiResponse<Todo>
//                {
//                    Success = true,
//                    Data = todo
//                });
//            }
//            catch (DbUpdateException e)
//            {
//                return BadRequest(new ApiResponse<Todo>
//                {
//                    Success = false,
//                    Data = null,
//                    Errors = e.Data,
//                    Message = "Erro ao recuperar Todo"
//                });
//            }
//        }

//        [HttpPut("v1/todos/{id:int}")]
//        public IActionResult Put(int id, TodoViewModel model)
//        {
//            try
//            {
//                var todo = _repository.Get(id);
//                if (todo == null)
//                {
//                    return NotFound(new ApiResponse<Todo>
//                    {
//                        Success = false,
//                        Message = "Erro ao recuperar Todo"
//                    });
//                }

//                todo.Title = model.Title;
//                todo.Description = model.Description;
//                todo.UserId = model.UserId;


//                _repository.Update(todo);
//                return Ok(new ApiResponse<Todo>
//                {
//                    Success = true,
//                    Data = todo,
//                    Message = "Todo atualizado"
//                });
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }

//        }


//        [HttpPost("v1/todos/")]
//        public IActionResult Post(TodoViewModel model)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest(new ApiResponse<Todo>
//                    {
//                        Success = false,
//                        Message = "Valide os dados novamente"
//                    });
//                }

//                var todo = new Todo
//                {
//                    Id = 0,
//                    Title = model.Title,
//                    Description = model.Description,
//                    Status = Status.Pending,
//                    CategoryId = model.CategoryId,
//                    UserId = model.UserId
//                };

//                _repository.Add(todo);
//                return Ok(new ApiResponse<Todo>
//                {
//                    Success = true,
//                    Data = todo,
//                    Message = "Todo criado com sucesso"
//                });
//            }
//            catch (DbUpdateException e)
//            {
//                return BadRequest(new ApiResponse<Todo>
//                {
//                    Success = false,
//                    Message = e.Message,
//                    Errors = e.Data

//                });
//            }
//        }

//        [HttpDelete("v1/todos/{id:int}")]
//        public IActionResult Delete(int id)
//        {
//            try
//            {
//                var todo = _repository.Get(id);
//                if (todo == null)
//                {
//                    return NotFound(new ApiResponse<Todo>
//                    {
//                        Success = false,
//                        Message = "Erro não recuperar Todo"
//                    });
//                }

//                _repository.Delete(todo);
//                return Ok(new ApiResponse<Todo>
//                {
//                    Success = true,
//                    Data = todo,
//                    Message = "Todo excluido com sucesso"
//                });
//            }
//            catch (DbUpdateException e)
//            {
//                return BadRequest(new ApiResponse<Todo>
//                {
//                    Success = false,
//                    Errors = e.Data,
//                    Message = "Erro ao excluir todo"
//                });
//            }
//        }


//        [HttpGet("v1/todosUser/{userId:int}")]
//        public IActionResult GetTodoByUser(int userId)
//        {
//            // Recuperar o ID do usuário a partir das claims
//            var usuarioId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


//            if (usuarioId == null)
//            {
//                return NotFound(new ApiResponse<Todo>
//                {
//                    Success = false,
//                    Message = "Usuário não encontrado"
//                });
//            }


//            try
//            {
//                var todos = _repository.GetByUser(usuarioId);

//                return Ok(new ApiResponse<IEnumerable<Todo>>
//                {
//                    Success = true,
//                    Data = todos,
//                    Message = "Lista Todo por usuário"
//                });
//            }
//            catch (Exception e)
//            {
//                return BadRequest(new ApiResponse<Todo>
//                {
//                    Success = false,
//                    Errors = e.Data,
//                    Message = e.Message
//                });
//            }
//        }
//    }
//}
