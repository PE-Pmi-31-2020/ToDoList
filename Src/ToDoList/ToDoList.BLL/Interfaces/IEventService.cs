using System.Collections.Generic;
using ToDoList.BLL.DTO;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IEventService
    {
        System.Threading.Tasks.Task CreateEventAsync(EventDto eventt);
        System.Threading.Tasks.Task EditEventAsync(EventDto eventt);
        System.Threading.Tasks.Task DeleteEventAsync(EventDto eventt);
        System.Threading.Tasks.Task DeleteEventAsync(int id);
        IEnumerable<Event> GetEventsByUserId(int? id);
    }
}
