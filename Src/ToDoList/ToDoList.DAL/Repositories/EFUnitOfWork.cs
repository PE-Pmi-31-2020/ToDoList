using System;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DataBase _db;
        private TaskRepository _taskRepository;
        private UserRepository _userRepository;
        private EventRepository _eventRepository;
        public EFUnitOfWork()
        {
            _db = new DataBase();
        }

        public IRepository<Task> Tasks => _taskRepository ?? (_taskRepository = new TaskRepository(_db));

        public IRepository<User> Users => _userRepository ?? (_userRepository = new UserRepository(_db));

        public IRepository<Event> Events => _eventRepository ?? (_eventRepository = new EventRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }
        public async System.Threading.Tasks.Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;

            if (disposing)
            {
                _db.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
