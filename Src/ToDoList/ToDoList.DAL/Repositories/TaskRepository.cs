using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class TaskRepository : IRepository<Task,int>
    {
        private readonly DataBase _db;

        public TaskRepository(DataBase dataBase)
        {
            _db = dataBase;
        }
        public void Create(Task item)
        {
            _db.Tasks.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = _db.Tasks.Find(id);
            if(toDelete!=null)
                _db.Tasks.Remove(toDelete);
            _db.SaveChanges();
        }

        public IEnumerable<Task> Find(Func<Task, bool> predicate)
        {
            return _db.Tasks.Include(t => t.User).AsEnumerable().Where(predicate).ToList();
        }

        public Task Get(int id)
        {
            return _db.Tasks.Find(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _db.Tasks.Include(t=>t.User).AsNoTracking().ToList();
        }

        public void Update(Task item)
        {
            var taskToUpdate = _db.Tasks.SingleOrDefault(t => t.Id == item.Id);
            if (taskToUpdate == null)
            {
                return;
            }
            taskToUpdate.Deadline = item.Deadline;
            taskToUpdate.UserId = item.UserId;
            taskToUpdate.User = item.User;
            taskToUpdate.Name = item.Name;
            taskToUpdate.IsDone = item.IsDone;
            _db.SaveChanges();
        }
    }
}
