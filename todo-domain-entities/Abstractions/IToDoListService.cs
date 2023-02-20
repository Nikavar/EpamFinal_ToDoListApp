using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using todo_domain_entities.POCO;

namespace todo_domain_entities.Abstractions
{
    public interface IToDoListService
    {
        List<ToDoListServiceModel> GetAllAsync();
        Task<ToDoList> GetByIdAsync(int id);
        Task CreateAsync(ToDoList todo);
        Task UpdateAsync(ToDoList todo);
        Task DeleteAsync(ToDoList todo);
        Task<MyTask> GetDetailsByIdAsync(int id);
    }
}
