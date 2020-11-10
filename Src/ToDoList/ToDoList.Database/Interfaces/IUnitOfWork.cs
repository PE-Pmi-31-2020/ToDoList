using ToDoList.Database.Entities;

namespace ToDoList.Database.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Task> Tasks { get; }
        IRepository<User> Users { get; }
        IRepository<Event> Events { get; }
        void Save();
        void Dispose();
    }
}
