using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Logic.DTO;

namespace ToDoList.Logic.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task CreateTaskAsync(TaskDto task);
        System.Threading.Tasks.Task EditTaskAsync(TaskDto task);
        System.Threading.Tasks.Task DeleteTaskAsync(TaskDto task);
    }
}


