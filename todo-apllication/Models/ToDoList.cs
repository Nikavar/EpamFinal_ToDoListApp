using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_apllication.Models
{
    public class ToDoList
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "U have to enter a List Title!")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }
        public int TaskCount { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public List<MyTask> MyTasks { get; set; }
    }
}
