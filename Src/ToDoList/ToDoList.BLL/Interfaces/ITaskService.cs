using System.Collections.Generic;
using ToDoList.BLL.DTO;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task CreateTaskAsync(TaskDto task);
        System.Threading.Tasks.Task EditTaskAsync(TaskDto task);
        System.Threading.Tasks.Task DeleteTaskAsync(TaskDto task);
        IEnumerable<Task> GetTasksByUserId(int? id);
        Task GetTaskById(int? id);
    }
}


