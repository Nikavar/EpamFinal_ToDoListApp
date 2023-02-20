using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todo_apllication.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string TaskStatus { get; set; }
        public List<MyTask> myTask { get; set; }
    }
}
