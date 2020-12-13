using Moq;
using NUnit.Framework;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests.Services
{
    public class EventServiceTests
    {
        private EventService _eventService;
        private static Mock<IUnitOfWork> _repository;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUnitOfWork>();
            _repository.Setup(rep => rep.SaveAsync());
            _eventService = new EventService(_repository.Object);
        }

        [Test]
        public void CreateEventAsyncTest()
        {
            _repository.Setup(rep => rep.Events.Create(It.IsAny<Event>()));
            _eventService.CreateEventAsync(new EventDto());
            _repository.Verify();
        }
        [Test]
        public void EditEventAsyncTest()
        {
            _repository.Setup(rep => rep.Events.Update(It.IsAny<Event>()));
            _eventService.EditEventAsync(new EventDto());
            _repository.Verify();
        }
        [Test]
        public void DeleteEventAsyncTest()
        {
            _repository.Setup(rep => rep.Events.Delete(It.IsAny<int>()));
            _eventService.DeleteEventAsync(new EventDto());
            _repository.Verify();
        }
    }
}