using TA.Entities.Models;

namespace TA.Contracts
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllTodosAsync();
        Task AddTodoAsync(Todo todo);
        Task<Todo> GetTodoById(int id);
        Task DeleteTodoAsync(int id);
    }
}
