using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace todo_domain_entities.POCO
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string TaskStatus { get; set; }
        public List<MyTask> myTask { get; set; }
    }
}
