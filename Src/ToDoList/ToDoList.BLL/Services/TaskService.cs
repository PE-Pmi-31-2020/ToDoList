using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories;
using ToDoList.DAL.Interfaces;

namespace ToDoList.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _database;

        public TaskService()
        {
            _database = new EFUnitOfWork();
        }
        public TaskService(IUnitOfWork repository)
        {
            _database = repository;
        }
        public async System.Threading.Tasks.Task CreateTaskAsync(TaskDto task)
        {
            _database.Tasks.Create(new Task()
            { 
                Id = task.Id,
                Name = task.Name,
                Deadline = task.Deadline,
                UserId = task.UserId
            });
            await _database.SaveAsync();
        }

        public async System.Threading.Tasks.Task EditTaskAsync(TaskDto task)
        {
            _database.Tasks.Update(new Task()
            {
                Id = task.Id,
                Name = task.Name,
                Deadline = task.Deadline,
                UserId = task.UserId
            });
            await _database.SaveAsync();
        }
        public async System.Threading.Tasks.Task DeleteTaskAsync(TaskDto task)
        {
            _database.Tasks.Delete(task.Id);
            await _database.SaveAsync();
        }
    }
}
