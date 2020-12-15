using System.Collections.Generic;
using System.Linq;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.BLL.Services
{
    public class EventService: IEventService
    {
        private readonly IUnitOfWork _database;

        public EventService()
        {
            _database = new EfUnitOfWork();
        }
        public EventService(IUnitOfWork repository)
        {
            _database = repository;
        }

        public async Task CreateEventAsync(EventDto eventt)
        {
            _database.Events.Create(new Event
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
            _database.Events.Update(new Event
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

        public async Task DeleteEventAsync(int id)
        {
            _database.Events.Delete(id);
            await _database.SaveAsync();
        }

        public IEnumerable<Event> GetEventsByUserId(int? id)
        {
            return id == null ? null : _database.Events.GetAll().Where(e => e.UserId == id);
        }
    }
}
