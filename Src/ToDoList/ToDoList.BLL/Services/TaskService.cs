using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _database;

        public TaskService()
        {
            _database = new EfUnitOfWork();
        }
        public TaskService(IUnitOfWork repository)
        {
            _database = repository;
        }
        public async Task CreateTaskAsync(TaskDto task)
        {
            _database.Tasks.Create(new DAL.Entities.Task
            { 
                Id = task.Id,
                Name = task.Name,
                Deadline = task.Deadline,
                UserId = task.UserId
            });
            await _database.SaveAsync();
        }

        public async Task EditTaskAsync(TaskDto task)
        {
            _database.Tasks.Update(new DAL.Entities.Task
            {
                Id = task.Id,
                Name = task.Name,
                Deadline = task.Deadline,
                UserId = task.UserId
            });
            await _database.SaveAsync();
        }
        public async Task DeleteTaskAsync(TaskDto task)
        {
            _database.Tasks.Delete(task.Id);
            await _database.SaveAsync();
        }

        public IEnumerable<DAL.Entities.Task> GetTasksByUserId(int? id)
        {
            return id == null ? null : _database.Tasks.GetAll().Where(t => t.UserId == id);
        }

        public DAL.Entities.Task GetTaskById(int? id)
        {
            return id == null ? null : _database.Tasks.Get((int)id);
        }
    }
}
