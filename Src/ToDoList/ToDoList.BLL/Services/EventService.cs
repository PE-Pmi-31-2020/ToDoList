using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class EventService: IEventService
    {
        private readonly IUnitOfWork _database;

        public EventService()
        {
            _database = new EFUnitOfWork();
        }
        public EventService(IUnitOfWork _repository)
        {
            _database = _repository;
        }

        public async System.Threading.Tasks.Task CreateEventAsync(EventDto eventt)
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

        public async System.Threading.Tasks.Task EditEventAsync(EventDto eventt)
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
        public async System.Threading.Tasks.Task DeleteEventAsync(EventDto eventt)
        {
            _database.Events.Delete(eventt.Id);
            await _database.SaveAsync();
        }
    }
}
