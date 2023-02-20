using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_apllication.Infrastructure;
using todo_apllication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using todo_domain_entities.Implementations;
using Mapster;
using todo_domain_entities.Models;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Context;

namespace todo_apllication.Controllers
{
    public class MyTaskController : Controller
    {
        private readonly IMyTaskService _service;
        private readonly ToDoContext _context;

        public MyTaskController(IMyTaskService service, ToDoContext context)
        {
            _service = service;
            _context = context;
        }

        public IList<MyTask> MyTask { get; set; }

        //GET //todo/details/3
        public async Task<IActionResult> Index(int id)
        {
            var MyTask = await _service.GetTasksByListIdAsync(id);

            if (MyTask.Count == 0)
            {
                TempData["Error"] = "There are No tasks in Progress!";
            }

            ViewData["testId"] = id;

            ViewData["taskCount"] = MyTask.Count;

            return View(MyTask);
        }

        public async Task<IActionResult> CurrentTasks()
        {
            var currentTasks = await _service.GetAllAsync();

            if (currentTasks.Count == 0)
            {
                TempData["Error"] = "There are No tasks in Progress!";
            }

            return View(currentTasks);
        }

        public async Task<IActionResult> CompletedTasks()
        {
            var completedTasks = await _service.GetCompletedTasksAsync();

            if (completedTasks.Count == 0)
            {
                TempData["Error"] = "There are No tasks in Progress!";
            }

            return View(completedTasks);
        }

        public async Task<IActionResult> RejectedTasks()
        {
            var rejectedTasks = await _service.GetRejectedTasksAsync();

            if (rejectedTasks.Count == 0)
            {
                TempData["Error"] = "There are No tasks in Progress!";
            }

            return View(rejectedTasks);
        }

        // GET/todo/create
        public IActionResult Create(int id)
        {
            ViewData["ListId"] = new SelectList(_context.ToDoLists, "Id", "Title",id.ToString());
            ViewData["PriorityId"] = new SelectList(_context.Priorities, "PriorityId", "TaskPriority", "1");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "TaskStatus", "2");

            return View();
        }

        // POST/todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyTask item)
        {
            if (ModelState.IsValid)
            {
                item.Id = 0;
                item.CreateTime = DateTime.Now;
                item.StatusId = 2;

                var taskCounter = (from list in _context.ToDoLists
                                 where list.Id == item.ListId
                                 select list).ToList().Take(1).FirstOrDefault();

                taskCounter.TaskCount++;

                await _service.CreateAsync(item.Adapt<todo_domain_entities.POCO.MyTask>());

                TempData["Success"] = "The Task has been added!";

                return RedirectToAction("Index", "MyTask", new { id = item.ListId});
            }

            return View("Views/MyTask/Index", Index((int)item.ListId));
        }

        // GET mytask/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.MyTasks.FindAsync(id);
            //var mytask = item.Adapt<MyTask>();
            //var result = await _service.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            ViewData["ListId"] = new SelectList(_context.ToDoLists, "Id", "Title", id.ToString());
            ViewData["PriorityId"] = new SelectList(_context.Priorities, "PriorityId", "TaskPriority", "1");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "TaskStatus", "2");
            ViewData["_listId"] = item.ListId;

            return View(item);
        }

        // POST /mytask/edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MyTask item)
        {
            if (ModelState.IsValid)
            {
                item.CreateTime = DateTime.Now;
                await _service.UpdateAsync(item.Adapt<MyTaskServiceModel>());

                TempData["Success"] = "The Task has Updated!";

                return RedirectToAction("Index", "MyTask", new { id = item.ListId });
            }

            return View(item);
        }

        // GET /mytask/delete/3
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var item = await _context.MyTasks.FindAsync(id);

            if (item == null)
            {
                TempData["Error"] = "The Task does not exist!";
            }

            else
            {
                var taskCounter = (from list in _context.ToDoLists
                                   where list.Id == item.ListId
                                   select list).ToList().Take(1).FirstOrDefault();

                taskCounter.TaskCount--;

                await _service.DeleteAsync(item);
                TempData["Success"] = "The Task has been deleted!";
            }

            return RedirectToAction("Index", "MyTask", new { id = item.ListId });
        }

        // GET //todo/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await _service.GetDetailsByIdAsync((int)id);
            ViewData["detailId"] = await (from i in _context.MyTasks
                                          where i.Id == id
                                          select i.ListId).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        public async Task<ActionResult> Complete(int id)
        {
            await _service.CompleteTask(id);
            return RedirectToAction("CurrentTasks", "MyTask");
        }

        public async Task<ActionResult> Activate(int id)
        {
            await _service.ActivateTask(id);
            return RedirectToAction("RejectedTasks", "MyTask");           
        }

        public async Task<ActionResult> Reject(int id)
        {
            await _service.RejectTask(id);
            return RedirectToAction("CurrentTasks", "MyTask");
        }
    }
}
