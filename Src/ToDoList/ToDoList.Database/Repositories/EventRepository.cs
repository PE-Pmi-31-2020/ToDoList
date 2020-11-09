using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;
using ToDoList.Database.Interfaces;

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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Event Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Event item)
        {
            throw new NotImplementedException();
        }
    }
}
