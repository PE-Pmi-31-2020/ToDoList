using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using Task = System.Threading.Tasks.Task;
namespace ToDoList.Logic.Interfaces
{
    interface IEventService
    {
        Task CreateEventAsync(Event eventt);
        Task EditEventAsync(Event eventt);
        Task DeleteEventAsync(Event eventt);
    }
}
