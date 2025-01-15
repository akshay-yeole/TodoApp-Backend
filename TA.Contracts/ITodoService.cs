using TA.Entities.Models;

namespace TA.Contracts
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllTodosAsync();
    }
}
