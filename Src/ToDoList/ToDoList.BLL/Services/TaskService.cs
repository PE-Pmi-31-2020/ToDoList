using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Database.Repositories;
using ToDoList.Logic.DTO;
using ToDoList.Logic.Interfaces;

namespace ToDoList.Logic.Services
{
    public class TaskService : ITaskService
    {
        private EFUnitOfWork _database;

        public TaskService()
        {
            _database = new EFUnitOfWork();
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(TaskDto task)
        {
            _database.Tasks.Create(new Database.Entities.Task()
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
            _database.Tasks.Update(new Database.Entities.Task()
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
