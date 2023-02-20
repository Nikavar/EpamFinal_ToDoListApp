using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Context;
using todo_domain_entities.POCO;

namespace todo_domain_entities.Implementations
{
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoContext _context;
        public ToDoListService(ToDoContext context)
        {
            _context = context;
        }
        public List<ToDoListServiceModel> GetAllAsync()
        {
            var allLists = from i in _context.ToDoLists orderby i.Id select i;
            return allLists.Adapt<List<ToDoListServiceModel>>();
        }
        public async Task<ToDoList> GetByIdAsync(int id)
        {
            return await _context.ToDoLists.FindAsync(id);
        }
        public async Task<MyTask> GetDetailsByIdAsync(int id)
        {
            var result = await _context.MyTasks.FindAsync(id);
            return result;
        }
        public async Task CreateAsync(ToDoList item)
        {
            var todoListToAdd = new ToDoList()
            {
                Title = item.Title,
                TaskCount = item.TaskCount                
            };

            await _context.ToDoLists.AddAsync(todoListToAdd);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ToDoList model)
        {
            var result = await _context.ToDoLists.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (result != null)
            {
                result.Title = model.Title;
                //result.TaskCount = model.TaskCount;

                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(ToDoList item)
        {
            _context.ToDoLists.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
