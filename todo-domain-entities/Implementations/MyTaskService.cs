using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Context;
using todo_domain_entities.Models;
using todo_domain_entities.POCO;

namespace todo_domain_entities.Implementations
{
    public class MyTaskService : IMyTaskService
    {
        private readonly ToDoContext _context;
        public MyTaskService(ToDoContext context)
        {
            _context = context;
        }
        public async Task<List<MyTask>> GetAllAsync()
        {
            var currentTasks = await _context.MyTasks
                                .Include(m => m.List)
                                .Include(m => m.TaskPriority)
                                .Include(m => m.TaskStatus)
                                .Where(t => t.StatusId == 2)
                                .ToListAsync();

            return currentTasks;
        }
        public async Task<MyTask> GetByIdAsync(int id)
        {
            var result = await _context.MyTasks
                        .Include(m => m.List)
                        .Include(m => m.TaskPriority)
                        .Include(m => m.TaskStatus)
                        .Where(t => t.Id == id)
                        .FirstOrDefaultAsync();

            return result;
        }
        public async Task<List<MyTask>> GetCompletedTasksAsync()
        {
            var completedTasks = await _context.MyTasks
                    .Include(m => m.List)
                    .Include(m => m.TaskPriority)
                    .Include(m => m.TaskStatus)
                    .Where(t => t.StatusId == 1)
                    .ToListAsync();

            return completedTasks;
        }
        public async Task<List<MyTask>> GetRejectedTasksAsync()
        {
            var rejectedTasks = await _context.MyTasks
                                .Include(m => m.List)
                                .Include(m => m.TaskPriority)
                                .Include(m => m.TaskStatus)
                                .Where(t => t.StatusId == 3)
                                .ToListAsync();

            return rejectedTasks;
        }
        public async Task CreateAsync(MyTask model)
        {
            var newTask = new MyTask()
            {
                Title = model.Title,
                CreateTime = model.CreateTime,
                Description = model.Description,
                DueDate = model.DueDate,
                PriorityId = model.PriorityId,
                StatusId = model.StatusId,
                ListId = model.ListId             
            };

            await _context.MyTasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(MyTaskServiceModel model)
        {
            var result = await _context.MyTasks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (result != null)
            {
                result.Title = model.Title;
                result.Description = model.Description;
                result.CreateTime = model.CreateTime;
                result.DueDate = model.DueDate;
                result.PriorityId = model.PriorityId;
                result.StatusId = model.StatusId;

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(MyTask item)
        {            
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MyTask>> GetTasksByListIdAsync(int id)
        {
            var myTask = await _context.MyTasks
                        .Include(m => m.List)
                        .Include(m => m.TaskPriority)
                        .Include(m => m.TaskStatus)
                        .Where(m => m.ListId == id)
                        .ToListAsync();

            return myTask;
        }
        public async Task<MyTask> GetDetailsByIdAsync(int? id)
        {
            var myTask = await _context.MyTasks
                        .Include(m => m.List)
                        .Include(m => m.TaskPriority)
                        .Include(m => m.TaskStatus)
                        .FirstOrDefaultAsync(m => m.Id == id);

            return myTask;
        }
        public async Task CompleteTask(int id)
        {
            var completedTask = await GetByIdAsync(id);
            completedTask.StatusId = 1;
            await UpdateAsync(completedTask.Adapt<MyTaskServiceModel>());
        }
        public async Task ActivateTask(int id)
        {
            var activatedTask = await GetByIdAsync(id);
            activatedTask.StatusId = 2;
            await UpdateAsync(activatedTask.Adapt<MyTaskServiceModel>());
        }
        public async Task RejectTask(int id)
        {
            var rejectedTask = await GetByIdAsync(id);
            rejectedTask.StatusId = 3;
            await UpdateAsync(rejectedTask.Adapt<MyTaskServiceModel>());
        }
    }
}
