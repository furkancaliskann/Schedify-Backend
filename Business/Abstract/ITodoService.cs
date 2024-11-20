using Business.Dtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(int id);
        Task AddAsync(CreateTodoDto entity);
        Task UpdateAsync(int id, UpdateTodoDto entity);
        Task DeleteAsync(int id);
    }
}
