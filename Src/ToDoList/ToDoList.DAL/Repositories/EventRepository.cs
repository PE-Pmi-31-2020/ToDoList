using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;
using ToDoList.Database.Interfaces;
using System.Data.Entity;

namespace ToDoList.Database.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        private DataBase db;
        public EventRepository(DataBase dataBase)
        {
            db = dataBase;
        }

        public void Create(Event item)
        {
            db.Events.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Event toDelete = db.Events.Find(id);
            if (toDelete != null)
                db.Events.Remove(toDelete);
            db.SaveChanges();
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return db.Events.Include(t => t.User).Where(predicate).ToList();
        }

        public Event Get(int id)
        {
            return db.Events.Find(id);
        }

        public IEnumerable<Event> GetAll()
        {
            return db.Events.Include(t => t.User).AsNoTracking().ToList();
        }

        public void Update(Event item)
        {
            var eventToUpdate = db.Events.SingleOrDefault(e => e.Id == item.Id);
            eventToUpdate.UserId = item.UserId;
            eventToUpdate.User = item.User;
            eventToUpdate.Description = item.Description;
            eventToUpdate.From = item.From;
            eventToUpdate.To = item.To;
            eventToUpdate.Name = item.Name;
            eventToUpdate.RemindTime = item.RemindTime;
            db.SaveChanges();
        }
    }
}
