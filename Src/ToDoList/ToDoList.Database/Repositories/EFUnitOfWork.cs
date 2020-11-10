using System;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;
using ToDoList.Database.Interfaces;

namespace ToDoList.Database.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataBase db;
        private TaskRepository taskRepository;
        private UserRepository userRepository;
        private EventRepository eventRepository;
        public EFUnitOfWork()
        {
            db = new DataBase();
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(db);
                return taskRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                if (eventRepository == null)
                    eventRepository = new EventRepository(db);
                return eventRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
