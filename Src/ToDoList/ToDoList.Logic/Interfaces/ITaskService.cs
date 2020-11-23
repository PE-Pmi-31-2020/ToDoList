using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using Task = System.Threading.Tasks.Task;
namespace ToDoList.Logic.Interfaces
{
    interface ITaskService
    {
        Task CreateTaskAsync(Database.Entities.Task task);
        Task EditTaskAsync(Database.Entities.Task task);
        Task DeleteTaskAsync(Database.Entities.Task task);
    }
}


