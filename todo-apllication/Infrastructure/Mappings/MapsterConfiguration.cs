using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_domain_entities;
using todo_domain_entities.POCO;

namespace todo_apllication.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<ToDoList, ToDoListServiceModel>
                .NewConfig()
                .TwoWays();
        }
    }
}
