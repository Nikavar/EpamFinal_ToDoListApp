using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using todo_domain_entities.Models;
using todo_domain_entities.POCO;

namespace todo_domain_entities.Abstractions
{
    public interface IMyTaskService
    {
        Task<List<MyTask>> GetAllAsync();
        Task<List<MyTask>> GetTasksByListIdAsync(int id);
        Task<MyTask> GetByIdAsync(int id);
        Task<List<MyTask>> GetCompletedTasksAsync();
        Task<List<MyTask>> GetRejectedTasksAsync();
        Task CreateAsync (MyTask model);
        Task UpdateAsync(MyTaskServiceModel model);        
        Task DeleteAsync(MyTask id);
        Task<MyTask> GetDetailsByIdAsync(int? id);
        Task CompleteTask(int id);
        Task RejectTask(int id);
        Task ActivateTask(int id);
    }
}
