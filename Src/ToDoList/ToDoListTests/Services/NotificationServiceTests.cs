using System;
using System.Collections.Generic;
using Moq;
using Notifications.Wpf;
using NUnit.Framework;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests.Services
{
     public class NotificationServiceTests
    {
        private INotificationService _eventService;
        private static Mock<IUnitOfWork> _repository;
        private static Mock<INotificationManager> _notification;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUnitOfWork>();
            _notification = new Mock<INotificationManager>();
            _eventService = new NotificationService(_repository.Object, _notification.Object);
        }

        [TestCase("des1",NotificationType.Success,"title1")]
        [TestCase("des2",NotificationType.Warning ,"title2")]
        [TestCase("des3", NotificationType.Information,"title3")]
        [TestCase("des4", NotificationType.Error, "title4")]
        public void ShowNotificationTest(string description,NotificationType type ,string title)
        {
            _notification.Setup(not => not.Show( It.IsAny<object>(),It.IsAny<string>(),It.IsAny<TimeSpan>(),It.IsAny<Action>(),It.IsAny<Action>()));
            _eventService.ShowNotification(description, type, title);
            _notification.Verify();
        }

        [Test]
        public void CreateTaskAsyncTest()
        {
            _repository.Setup(rep => rep.Tasks.GetAll()).Returns(GetTestTasks());
            _repository.Setup(rep => rep.Events.GetAll()).Returns(GetTestEvents());
            _eventService.RunNotificationKernel();
            _repository.Verify();
        }

        private IEnumerable<Task> GetTestTasks()
        {
            return new List<Task>()
            {
                new Task(){Id = 1, Deadline = new TimeSpan(1),Name = "Name1",User = new User(),UserId = 1},
                new Task(){Id = 2, Deadline = new TimeSpan(2),Name = "Name2",User = new User(),UserId = 2},
                new Task(){Id = 3, Deadline = new TimeSpan(3),Name = "Name3",User = new User(),UserId = 3},
            };
        }
        private IEnumerable<Event> GetTestEvents()
        {
            return new List<Event>()
            {
                new Event(){Id = 1, RemindTime = new TimeSpan(1),To = new TimeSpan(4) , Description = "d1", Name = "Name1",User = new User(),UserId = 1},
                new Event(){Id = 2, RemindTime = new TimeSpan(2),To = new TimeSpan(5) , Description = "d2",Name = "Name2",User = new User(),UserId = 2},
                new Event(){Id = 3, RemindTime = new TimeSpan(3),To = new TimeSpan(6) , Description = "d3",Name = "Name3",User = new User(),UserId = 3},
            };
        }

    }
}
