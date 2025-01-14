using TA.Contracts;
using TA.DataRepository;

namespace TA.Repositories
{
    public class TodoService : ITodoService
    {
        private readonly TodoAppContext _context;
        
        public TodoService(TodoAppContext context)
        {
            _context = context;
        }


    }
}
