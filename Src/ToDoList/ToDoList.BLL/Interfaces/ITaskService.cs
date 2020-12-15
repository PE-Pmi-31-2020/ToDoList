using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.DTO;

namespace ToDoList.BLL.Interfaces
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskDto task);
        Task EditTaskAsync(TaskDto task);
        Task DeleteTaskAsync(TaskDto task);
        IEnumerable<DAL.Entities.Task> GetTasksByUserId(int? id);
        DAL.Entities.Task GetTaskById(int? id);
    }
}


