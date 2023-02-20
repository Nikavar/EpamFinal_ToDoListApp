using System;
using System.Collections.Generic;
using System.Text;

namespace todo-domain.POCO
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TaskCount { get; set; }
        //public User User { get; set; }
        //public int? UserId { get; set; }
        //public List<MyTask> MyTasks { get; set; }
    }
}

