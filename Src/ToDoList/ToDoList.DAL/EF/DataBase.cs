using System.Data.Entity;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.EF
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
