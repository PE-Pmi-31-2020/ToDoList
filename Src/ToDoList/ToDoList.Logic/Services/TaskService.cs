using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Database.Repositories;
using ToDoList.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Logic.Services
{
    class TaskService : ITaskService
    {
        private EFUnitOfWork _database;

        public TaskService()
        {
            _database = new EFUnitOfWork();
        }

        public async Task CreateTaskAsync(Database.Entities.Task task)
        {
            _database.Tasks.Create(task);
            await _database.SaveAsync();
        }

        public async Task EditTaskAsync(Database.Entities.Task task)
        {
            _database.Tasks.Update(task);
            await _database.SaveAsync();
        }
        public async Task DeleteTaskAsync(Database.Entities.Task task)
        {
            _database.Tasks.Delete(task.Id);
            await _database.SaveAsync();
        }
    }
}
