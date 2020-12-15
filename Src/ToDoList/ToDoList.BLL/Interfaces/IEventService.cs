using System.Collections.Generic;
using ToDoList.BLL.DTO;
using ToDoList.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.BLL.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventDto eventt);
        Task EditEventAsync(EventDto eventt);
        Task DeleteEventAsync(EventDto eventt);
        Task DeleteEventAsync(int id);
        IEnumerable<Event> GetEventsByUserId(int? id);
    }
}
