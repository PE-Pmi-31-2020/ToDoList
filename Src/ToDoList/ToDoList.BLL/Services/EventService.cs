using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Database.Repositories;
using ToDoList.Logic.DTO;
using ToDoList.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Logic.Services
{
    public class EventService: IEventService
    {
        private EFUnitOfWork _database;

        public EventService()
        {
            _database = new EFUnitOfWork();
        }

        public async Task CreateEventAsync(EventDto eventt)
        {
            _database.Events.Create(new Event()
            {
                Description = eventt.Description,
                Id = eventt.Id,
                Name = eventt.Name,
                From = eventt.From,
                To = eventt.To,
                RemindTime = eventt.RemindTime,
                UserId = eventt.UserId
            });
            await _database.SaveAsync();
        }

        public async Task EditEventAsync(EventDto eventt)
        {
            _database.Events.Update(new Event()
            {
                Description = eventt.Description,
                Id = eventt.Id,
                Name = eventt.Name,
                From = eventt.From,
                To = eventt.To,
                RemindTime = eventt.RemindTime,
                UserId = eventt.UserId
            });
            await _database.SaveAsync();
        }
        public async Task DeleteEventAsync(EventDto eventt)
        {
            _database.Events.Delete(eventt.Id);
            await _database.SaveAsync();
        }
    }
}
