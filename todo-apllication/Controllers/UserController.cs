using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_apllication.Models;

namespace todo_apllication.Controllers
{
    public class UserController : Controller
    {
        // GET /User/
        public IActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }
    }
}
