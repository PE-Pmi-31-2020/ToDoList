using Moq;
using NUnit.Framework;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests.Services
{
    class TaskServiceTests
    {
        private TaskService _taskService;
        private static Mock<IUnitOfWork> _repository;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUnitOfWork>();
            _repository.Setup(rep => rep.SaveAsync());
            _taskService = new TaskService(_repository.Object);
        }

        [Test]
        public void CreateTaskAsyncTest()
        {
            _repository.Setup(rep => rep.Tasks.Create(It.IsAny<Task>()));
            _taskService.CreateTaskAsync(new TaskDto());
            _repository.Verify();
        }
        [Test]
        public void EditTaskAsyncTest()
        {
            _repository.Setup(rep => rep.Tasks.Update(It.IsAny<Task>()));
            _taskService.EditTaskAsync(new TaskDto());
            _repository.Verify();
        }
        [Test]
        public void DeleteTasksAsyncTest()
        {
            _repository.Setup(rep => rep.Tasks.Delete(It.IsAny<int>()));
            _taskService.DeleteTaskAsync(new TaskDto());
            _repository.Verify();
        }
    }
}
