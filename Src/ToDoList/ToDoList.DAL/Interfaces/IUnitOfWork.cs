using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Task,int> Tasks { get; }
        IRepository<User,string> Users { get; }
        IRepository<Event,int> Events { get; }
        void Save();
        System.Threading.Tasks.Task SaveAsync();
        void Dispose();
    }
}
