using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Database.Repositories;
using ToDoList.Logic.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.Logic.Services
{
    class EventService: IEventService
    {
        private EFUnitOfWork _database;

        public EventService()
        {
            _database = new EFUnitOfWork();
        }

        public async Task CreateEventAsync(Event eventt)
        {
            _database.Events.Create(eventt);
            await _database.SaveAsync();
        }

        public async Task EditEventAsync(Event eventt)
        {
            _database.Events.Update(eventt);
            await _database.SaveAsync();
        }
        public async Task DeleteEventAsync(Event eventt)
        {
            _database.Events.Delete(eventt.Id);
            await _database.SaveAsync();
        }
    }
}
