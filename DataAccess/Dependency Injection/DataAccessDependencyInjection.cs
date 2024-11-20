using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Dependency_Injection
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}
