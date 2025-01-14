using Microsoft.EntityFrameworkCore;
using TA.Entities.Models;

namespace TA.DataRepository
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions< TodoAppContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
