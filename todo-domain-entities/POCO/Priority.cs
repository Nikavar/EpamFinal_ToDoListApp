using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace todo_domain_entities.POCO
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }
        public string TaskPriority { get; set; }
        public List<MyTask> myTask { get; set; }
    }
}
