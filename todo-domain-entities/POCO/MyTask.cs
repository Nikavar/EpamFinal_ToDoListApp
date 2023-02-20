using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace todo_domain_entities.POCO
{
    public class MyTask
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateTime { get; set; }


        /*--------------------------------------*/
        public Status TaskStatus { get; set; }
        public int? StatusId { get; set; }
        public Priority TaskPriority { get; set; }
        public int? PriorityId { get; set; }
        public ToDoList List { get; set; }
        public int? ListId { get; set; }
    }
}
