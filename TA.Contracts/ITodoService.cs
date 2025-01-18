using TA.Entities.DTOs;
using TA.Entities.Models;

namespace TA.Contracts
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDTO>> GetAllTodosAsync();
        Task AddTodoAsync(TodoDTO todo);
        Task<TodoDTO> GetTodoById(int id);
        Task DeleteTodoAsync(int id);
        Task UpdateTodoAsync(int id, TodoDTO todo);
    }
}
