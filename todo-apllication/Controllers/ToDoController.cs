using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_apllication.Infrastructure;
using todo_apllication.Models;
using todo_domain_entities;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Context;

namespace todo_apllication.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoListService _service;

        private readonly ToDoContext _context;

        public ToDoController(IToDoListService service, ToDoContext context)
        {
            _service = service;
            _context = context;
        }

        // GET/todo/create
        public IActionResult Create() => View();

        // GET/todo/contact
        public IActionResult Contact() => View();

        // GET/todo/contact
        public IActionResult About() => View();       
        
        // GET
        public IActionResult Index()
        {
            var result = _service.GetAllAsync();
            if (result.Count == 0)
            {
                TempData["Error"] = "There are No tasks in Progress!";
            }

            var todoDTO = result.Adapt<List<ToDoList>>();

            return View(todoDTO);
        }

        // POST/todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(item);
                //await _context.SaveChangesAsync();
                await _service.CreateAsync(item.Adapt<todo_domain_entities.POCO.ToDoList>());

                TempData["Success"] = "The Task has been added!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET todo/edit/3
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result.Adapt<List<ToDoList>>());
        }

        // GET todo/edit/3
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _context.ToDoLists.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST /todo/edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(item.Adapt<todo_domain_entities.POCO.ToDoList>());
                TempData["Success"] = "The Task has Updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET /todo/delete/3
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var result = await _service.GetByIdAsync((int)id);
            var item = await _context.MyTasks.FirstOrDefaultAsync(i => i.ListId == id);


            if (result == null)
            {
                TempData["Error"] = "The Task does not exist!";
            }

            else
            {
                todo_domain_entities.POCO.ToDoList taskCounter = null;

                if (item != null)
                {
                    taskCounter = (from list in _context.ToDoLists
                                       where list.Id == item.ListId
                                       select list).ToList().Take(1).FirstOrDefault();

                    taskCounter.TaskCount--;
                }             

                await _service.DeleteAsync(result);
                TempData["Success"] = "The Task has been deleted!";
            }

            return RedirectToAction("Index");
        }

        // GET //todo/details/3
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var tasks = await _service.GetDetailsByIdAsync((int)id);

            if(tasks == null)
            {
                TempData["Error"] = "The Task does not exists!";
            }

            return View(tasks);
        }
    }
}
