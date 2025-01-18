using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TA.Contracts;
using TA.Core.AppExceptions;
using TA.DataRepository;
using TA.Entities.DTOs;
using TA.Entities.Models;

namespace TA.Repositories
{
    public class TodoService : ITodoService
    {
        private readonly TodoAppContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _loger;
        
        public TodoService(TodoAppContext context, IMapper mapper, ILogger<TodoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _loger = logger;
        }

        public async Task<IEnumerable<TodoDTO>> GetAllTodosAsync()
        {
            _loger.LogInformation("Executig Method GetAllTodosAsync()");
            var res = await _context.Todos.ToListAsync();
            if (res.Count == 0)
            {
                throw new NotFoundException("No Todos Found");
            }
            var mappedTodos = _mapper.Map<IEnumerable<TodoDTO>>(res);
            return mappedTodos;
        }

        public async Task<TodoDTO> GetTodoById(int id)
        {
            _loger.LogInformation("Executing Method GetAllTodosAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            var mappedTodo = _mapper.Map<TodoDTO>(res);
            return mappedTodo ?? throw new NotFoundException("No Todos Found");
        }

        public async Task AddTodoAsync(TodoDTO todo)
        {
            _loger.LogInformation("Executing Method AddTodoAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Title == todo.Title);
            if (res != null) {
                throw new ConflictException("Todo Already Exists");
            }
            var mappedTodo = _mapper.Map<Todo>(todo);
            await _context.AddAsync(mappedTodo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(int id, TodoDTO todo)
        {
            _loger.LogInformation("Executing Method UpdateTodoAsync()");
            var res = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
            {
                throw new NotFoundException("No Todos Found");
            }
            var mappedTodo = _mapper.Map<Todo>(todo);
            res.Title = mappedTodo.Title;
            res.IsCompleted = mappedTodo.IsCompleted;
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
