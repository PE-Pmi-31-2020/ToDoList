using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;
using ToDoList.Database.Interfaces;

namespace ToDoList.Database.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private DataBase db;

        public TaskRepository(DataBase dataBase)
        {
            db = dataBase;
        }
        public void Create(Task item)
        {
            db.Tasks.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Task toDelete = db.Tasks.Find(id);
            if(toDelete!=null)
                db.Tasks.Remove(toDelete);
            db.SaveChanges();
        }

        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
           return db.Tasks.Include(t => t.User).Where(predicate).ToList();
        }

        public Task Get(int id)
        {
            return db.Tasks.Find(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return db.Tasks.Include(t=>t.User).AsNoTracking().ToList();
        }

        public void Update(Task item)
        {
            db.Tasks.Update(item);
            db.SaveChanges();
        }
    }
}
