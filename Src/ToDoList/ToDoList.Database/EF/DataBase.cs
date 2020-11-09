using Microsoft.EntityFrameworkCore;
using ToDoList.Database.Entities;

namespace ToDoList.Database.EF
{
    public class DataBase : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
            Database.EnsureCreated();

        }
    }


}
