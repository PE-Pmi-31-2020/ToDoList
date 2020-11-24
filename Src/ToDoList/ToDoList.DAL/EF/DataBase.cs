using System;
using ToDoList.Database.Entities;
using System.Configuration;
using System.Data.Entity;

namespace ToDoList.Database.EF
{
    public class DataBase : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        public DataBase()
            : base("DbConnectionString")
        { }
    }


}
