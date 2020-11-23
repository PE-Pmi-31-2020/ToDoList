using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Logic.DTO;
using Task = System.Threading.Tasks.Task;
namespace ToDoList.Logic.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventDto eventt);
        Task EditEventAsync(EventDto eventt);
        Task DeleteEventAsync(EventDto eventt);
    }
}
