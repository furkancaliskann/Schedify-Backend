using Business.Abstract;
using Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Pagination;

namespace Web_API.Controllers
{
    [Route("api/todos")]
    [ApiController]
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
            var todos = await _todoService.GetAllAsync(paginationQuery);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById([FromRoute] int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            await _todoService.AddAsync(createTodoDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            await _todoService.UpdateAsync(id, updateTodoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _todoService.DeleteAsync(id);
            return NoContent();
        }
    }

}
