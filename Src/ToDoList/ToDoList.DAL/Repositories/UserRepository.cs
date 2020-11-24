using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return db.Users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(t => t.Tasks).ToList();
        }

        public void Update(User item)
        {
            var userToUpdate = db.Users.SingleOrDefault(u => u.Id == item.Id);
            userToUpdate.FullName = item.FullName;
            userToUpdate.Password = item.Password;
            userToUpdate.UserName = item.UserName;
            userToUpdate.Events = item.Events;
            userToUpdate.Tasks = item.Tasks;
            db.SaveChanges();
        }
    }
}
