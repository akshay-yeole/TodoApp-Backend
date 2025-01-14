
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
