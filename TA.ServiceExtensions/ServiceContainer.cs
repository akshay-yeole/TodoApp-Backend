
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TA.Contracts;
using TA.Repositories;

namespace TA.ServiceExtensions
{
    public static class ServiceContainer
    {
        //Configure Database
        public static void ConfigureDb<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
            options.UseSqlServer(
                configuration.GetConnectionString("Default"), 
                x => x.MigrationsAssembly("TA.WebApi"));
            });
        }

        //Configure Business Services
        public static void ConfigureBusinessService(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }
    }
}
