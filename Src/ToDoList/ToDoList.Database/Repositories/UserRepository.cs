using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;
using ToDoList.Database.Interfaces;

namespace ToDoList.Database.Repositories
{
    public class UserRepository:IRepository<User>
    {
        private DataBase db;

        public UserRepository(DataBase dataBase)
        {
            db = dataBase;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User toDelete = db.Users.Find(id);
            if (toDelete != null)
                db.Users.Remove(toDelete);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Include(t => t.Tasks).Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User Get(string userName)
        {
            return db.Users.Where(u => u.UserName == userName).First();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(t => t.Tasks).ToList();
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
