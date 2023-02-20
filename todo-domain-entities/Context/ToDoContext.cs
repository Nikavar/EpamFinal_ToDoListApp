using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.POCO;

namespace todo_domain_entities.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasOne<User>(l => l.User)
                .WithMany(u => u.MyLists)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MyTask>()
                .HasOne<ToDoList>(t => t.List)
                .WithMany(l => l.MyTasks)
                .HasForeignKey(t => t.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Priority>()
                .HasMany<MyTask>(p => p.myTask)
                .WithOne(t => t.TaskPriority)
                .HasForeignKey(p => p.PriorityId);

            modelBuilder.Entity<Status>()
                .HasMany<MyTask>(s => s.myTask)
                .WithOne(t => t.TaskStatus)
                .HasForeignKey(s => s.StatusId);
        }
    }
}
