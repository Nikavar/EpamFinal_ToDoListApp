using System;
using System.Collections.Generic;
using System.Text;

namespace todo_domain_entities.Models
{
    public class MyTaskServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateTime { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
    }
}
