using App.Api.Domain.Repositories;
using App.Api.Domain.Domain;
using App.Api.Domain.Responses.ApiResponses;
using App.Api.Web.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;

        public CategoryController(IRepository<Category> repository)
            => _repository = repository;

        [HttpGet("v1/categories/")]
        public IActionResult Get()
        {

            try
            {
                var categories = _repository.GetAll();

                return Ok(new ApiResponse<IEnumerable<Category>>
                {
                    Success = true,
                    Message = "Lista de Categorias",
                    Data = categories
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<IEnumerable<Category>>
                {
                    Success = false,
                    Message = "Categorias não encontradas",
                    Errors = ex.Data
                });
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public IActionResult GetById(int id)
        {


            try
            {
                var category = _repository.Get(id);

                if (category == null)
                {
                    return NotFound(new ApiResponse<Category>
                    {
                        Message = "Categoria não encontrada",
                        Data = null,
                        Success = false
                    });
                }

                return Ok(new ApiResponse<Category>
                {
                    Success = true,
                    Data = category
                });


            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<Category>
                {
                    Success = false,
                    Data = null,
                    Errors = new List<string>{e.Message},
                    Message = "Erro ao recuperar Categoria"
                });
            }
        }

        [HttpPost("v1/categories")]
        public IActionResult Post(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Category>
                {
                    Success = false,
                    Message = "Erro ",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var category = new Category
            {
                Id = 0,
                Name = model.Name
            };


            _repository.Add(category);

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, new ApiResponse<Category>
            {
                Success = true,
                Message = "Categoria criada com Sucesso !",
                Data = category
            });

        }


        [HttpPut("v1/categories/{id:int}")]
        public IActionResult Put(int id, CategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                BadRequest(new ApiResponse<Category>
                {
                    Success = false,
                    Message = "Dados inválidos"
                });
            }

            var category = _repository.Get(id);

            category.Name = model.Name;

            _repository.Update(category);

            return Ok(new ApiResponse<Category>
            {
                Data = category,
                Success = true,
                Message = "Categoria atualizada"
            });
        }

        [HttpDelete("v1/categories/{id:int}")]
        public IActionResult Delete(int id)
        {

            try
            {
                var category = _repository.Get(id);

                if (category == null)
                {
                    return NotFound(new ApiResponse<Category>
                    {
                        Message = "Categoria não encontrada",
                        Data = null,
                        Success = false
                    });
                }

                _repository.Delete(id);

                return Ok(new ApiResponse<Category>
                {
                    Success = true,
                    Message = "Categoria excluída",

                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResponse<Category>
                {
                    Success = false,
                    Errors = new List<string>{e.Message},
                    Message = "Erro ao Excluir Categoria"
                    
                });
            }

        }

    }
}
