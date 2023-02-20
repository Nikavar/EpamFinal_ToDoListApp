using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Implementations;

namespace todo_apllication.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IToDoListService, ToDoListService>();
            services.AddTransient<IMyTaskService, MyTaskService>();
        }
    }
}
