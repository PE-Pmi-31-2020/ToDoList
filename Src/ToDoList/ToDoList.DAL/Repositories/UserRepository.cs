using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class UserRepository:IRepository<User,string>
    {
        private readonly DataBase _db;

        public UserRepository(DataBase dataBase)
        {
            _db = dataBase;
        }
        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public void Delete(int id)
        {
            var toDelete = _db.Users.Find(id);
            if (toDelete != null)
                _db.Users.Remove(toDelete);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Include(t => t.Tasks).AsEnumerable().Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string userName)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.Include(t => t.Tasks).ToList();
        }

        public void Update(User item)
        {
            var userToUpdate = _db.Users.SingleOrDefault(u => u.Id == item.Id);
            if (userToUpdate == null)
            {
                return;
            }
            userToUpdate.FullName = item.FullName;
            userToUpdate.Password = item.Password;
            userToUpdate.UserName = item.UserName;
            userToUpdate.Events = item.Events;
            userToUpdate.Tasks = item.Tasks;
            _db.SaveChanges();
        }
    }
}
