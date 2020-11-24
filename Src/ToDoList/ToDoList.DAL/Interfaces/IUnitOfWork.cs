using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Task> Tasks { get; }
        IRepository<User> Users { get; }
        IRepository<Event> Events { get; }
        void Save();
        System.Threading.Tasks.Task SaveAsync();
        void Dispose();
    }
}
