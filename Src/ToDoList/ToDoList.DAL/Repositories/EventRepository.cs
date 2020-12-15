using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class EventRepository : IRepository<Event,int>
    {
        private readonly DataBase _db;
        public EventRepository(DataBase dataBase)
        {
            _db = dataBase;
        }

        public void Create(Event item)
        {
            _db.Events.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = _db.Events.Find(id);
            if (toDelete != null)
                _db.Events.Remove(toDelete);
            _db.SaveChanges();
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return _db.Events.Include(t => t.User).AsEnumerable().Where(predicate).ToList();
        }

        public Event Get(int id)
        {
            return _db.Events.Find(id);
        }

        public IEnumerable<Event> GetAll()
        {
            return _db.Events.Include(t => t.User).AsNoTracking().ToList();
        }

        public void Update(Event item)
        {
            var eventToUpdate = _db.Events.SingleOrDefault(e => e.Id == item.Id);
            if (eventToUpdate == null)
            {
                return;
            }
            eventToUpdate.UserId = item.UserId;
            eventToUpdate.User = item.User;
            eventToUpdate.Description = item.Description;
            eventToUpdate.From = item.From;
            eventToUpdate.To = item.To;
            eventToUpdate.Name = item.Name;
            eventToUpdate.RemindTime = item.RemindTime;
            _db.SaveChanges();
        }
    }
}
