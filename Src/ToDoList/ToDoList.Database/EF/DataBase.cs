using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.Entities;
using Microsoft.Extensions.Configuration;
using System.Configuration;
namespace ToDoList.Database.EF
{
    public class DataBase : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DataBase()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString);
            optionsBuilder.UseSqlite("Data Source=../../../../../DataBase.db");

        }
    }


}
