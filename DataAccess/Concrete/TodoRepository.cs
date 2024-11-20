using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class TodoRepository : ITodoRepository
    {
        private readonly SchedifyContext _context;

        public TodoRepository(SchedifyContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Todo entity)
        {
            await _context.Todos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task UpdateAsync(Todo entity)
        {
            _context.Todos.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
