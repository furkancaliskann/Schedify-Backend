using Business.Abstract;
using Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Pagination;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Web_API.Controllers
{
    [Route("api/todos")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] PaginationQuery paginationQuery)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found!");
            }

            var todos = await _todoService.GetAllAsync(userId, paginationQuery);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found!");
            }

            var todo = await _todoService.GetByIdAsync(userId, id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) 
            {
                return Unauthorized("User not found!");
            }

            await _todoService.AddAsync(createTodoDto, userId);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found!");
            }

            await _todoService.UpdateAsync(userId, id, updateTodoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not found!");
            }

            await _todoService.DeleteAsync(userId, id);
            return NoContent();
        }
    }

}
