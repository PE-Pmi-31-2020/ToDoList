using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Logic.DTO;
using Task = System.Threading.Tasks.Task;
namespace ToDoList.Logic.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskDto task);
        Task EditTaskAsync(TaskDto task);
        Task DeleteTaskAsync(TaskDto task);
    }
}


