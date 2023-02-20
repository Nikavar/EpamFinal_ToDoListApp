using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_domain_entities.Context;
using todo_domain_entities.POCO;

namespace todo_apllication.Models.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ToDoContext>();
                context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }


                if (!context.Priorities.Any())
                {
                    context.Priorities.AddRange(
                        new Priority { TaskPriority = "Low" },
                        new Priority { TaskPriority = "Med" },
                        new Priority { TaskPriority = "High" }
                    );

                    context.SaveChanges();
                };

                if(!context.Statuses.Any())
                {
                    context.Statuses.AddRange(
                        new Status { TaskStatus = "Rejected" },                        
                        new Status { TaskStatus = "InProgress"},
                        new Status { TaskStatus = "Completed"}                
                      );

                    context.SaveChanges();
                }
            }
        }
    }
}
