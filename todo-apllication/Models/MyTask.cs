using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todo_apllication.Models
{
    public class MyTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
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
