using Business.Abstract;
using Business.Dtos;
using Business.FluentValidation.Todo;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Dependency_Injection
{
    public static class BusinessDependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IValidator<CreateTodoDto>, CreateTodoDtoValidator>();
            services.AddScoped<IValidator<UpdateTodoDto>, UpdateTodoDtoValidator>();
            return services;
        }
    }
}
