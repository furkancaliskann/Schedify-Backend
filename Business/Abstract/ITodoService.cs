using Business.Dtos;
using DataAccess.Pagination;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITodoService
    {
        Task<PagedResponse<Todo>> GetAllAsync(string userId, PaginationQuery paginationQuery);
        Task<Todo> GetByIdAsync(string userId, int id);
        Task AddAsync(CreateTodoDto entity, string userId);
        Task UpdateAsync(string userId, int todoId, UpdateTodoDto entity);
        Task DeleteAsync(string userId, int id);
    }
}
