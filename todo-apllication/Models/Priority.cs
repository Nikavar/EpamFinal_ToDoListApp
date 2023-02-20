using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_apllication.Models
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }
        public string TaskPriority { get; set; }
        public List<MyTask> myTask { get; set; }
    }
}
