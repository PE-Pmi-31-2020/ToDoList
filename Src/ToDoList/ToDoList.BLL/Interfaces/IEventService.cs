using ToDoList.BLL.DTO;

namespace ToDoList.BLL.Interfaces
{
    public interface IEventService
    {
        System.Threading.Tasks.Task CreateEventAsync(EventDto eventt);
        System.Threading.Tasks.Task EditEventAsync(EventDto eventt);
        System.Threading.Tasks.Task DeleteEventAsync(EventDto eventt);
    }
}
