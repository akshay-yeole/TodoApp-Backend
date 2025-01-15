using Microsoft.EntityFrameworkCore;
using TA.Contracts;
using TA.Core.AppExceptions;
using TA.DataRepository;
using TA.Entities.Models;

namespace TA.Repositories
{
    public class TodoService : ITodoService
    {
        private readonly TodoAppContext _context;
        
        public TodoService(TodoAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            var res = await _context.Todos.ToListAsync();
            if (res.Count == 0)
            {
                throw new NotFoundException("No todos found");
            }
            return res;
        }
    }
}
