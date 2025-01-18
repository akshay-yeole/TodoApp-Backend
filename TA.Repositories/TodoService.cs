using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TA.Contracts;
using TA.Core.AppExceptions;
using TA.DataRepository;
using TA.Entities.Models;

namespace TA.Repositories
{
    public class TodoService : ITodoService
    {
        private readonly ILogger<TodoService> _loger;
        private readonly TodoAppContext _context;
        
        public TodoService(TodoAppContext context, ILogger<TodoService> logger)
        {
            _context = context;
            _loger = logger;
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            _loger.LogInformation("Executig Method GetAllTodosAsync()");
            var res = await _context.Todos.ToListAsync();
            if (res.Count == 0)
            {
                throw new NotFoundException("No Todos Found");
            }
            return res;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            _loger.LogInformation("Executing Method GetAllTodosAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            return res ?? throw new NotFoundException("No Todos Found");
        }

        public async Task AddTodoAsync(Todo todo)
        {
            _loger.LogInformation("Executing Method AddTodoAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Title == todo.Title);
            if (res != null) {
                throw new ConflictException("Todo Already Exists");
            }

            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(int id, Todo todo)
        {
            _loger.LogInformation("Executing Method UpdateTodoAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                throw new NotFoundException("No Todos Found");
            }
            res.Title = todo.Title;
            res.IsCompleted = todo.IsCompleted;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(int id)
        {
            _loger.LogInformation("Executing Method DeleteTodoAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                throw new NotFoundException("No Todos Found");
            }

            _context.Todos.Remove(res);
            await _context.SaveChangesAsync();
        }
    }
}
