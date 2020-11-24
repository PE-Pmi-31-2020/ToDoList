using ToDoList.BLL.DTO;

namespace ToDoList.BLL.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task CreateTaskAsync(TaskDto task);
        System.Threading.Tasks.Task EditTaskAsync(TaskDto task);
        System.Threading.Tasks.Task DeleteTaskAsync(TaskDto task);
    }
}


