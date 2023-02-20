using System;

namespace todo_domain_entities
{
    public class ToDoListServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TaskCount { get; set; }
    }
}
