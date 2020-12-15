using Moq;
using NUnit.Framework;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests.Services
{
    public class UserServiceTests
    {
        private UserService _userService;
        private static Mock<IUnitOfWork> _repository;
        private static Mock<ICryptService> _cryptServiceMock;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUnitOfWork>();
            _cryptServiceMock = new Mock<ICryptService>();
            _repository.Setup(rep => rep.Save());
            _userService = new UserService(_repository.Object, _cryptServiceMock.Object);
        }

        [TestCase("user1", "user1","pas1","pas2")]
        [TestCase("user2", "user2", "pas1", "pas2")]
        [TestCase("user3", "user3", "pas1", "pas2")]
        public void CreateUser_DifferentPasswordsException_Test(string fullName, string userName, string password, string repeatedPassword)
        {
            var result = _userService.CreateUser(fullName, userName, password, repeatedPassword);

            Assert.AreEqual(result, Errors.Password);
        }

        [TestCase("user1", "user1", "pas1", "pas1")]
        [TestCase("user2", "user2", "pas1", "pas1")]
        [TestCase("user3", "user3", "pas1", "pas1")]
        public void CreateUser_UserIsExistsException_Test(string fullName, string userName, string password, string repeatedPassword)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User());

            var result = _userService.CreateUser(fullName, userName, password, repeatedPassword);

            Assert.AreEqual(result, Errors.UserExists);
        }

        [TestCase("user1", "user1", "pas1", "pas1")]
        [TestCase("user2", "user2", "pas1", "pas1")]
        [TestCase("user3", "user3", "pas1", "pas1")]
        public void CreateUser_Successful_Test(string fullName, string userName, string password, string repeatedPassword)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns((User)null);
            _repository.Setup(rep => rep.Users.Create(It.IsAny<User>()));
            _cryptServiceMock.Setup(s => s.Encrypt(It.IsAny<string>())).Returns(password);

           var result = _userService.CreateUser(fullName, userName, password, repeatedPassword);

            _repository.Verify();
            _cryptServiceMock.Verify();
            Assert.AreEqual(result,Errors.Success);
            
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_DifferentPasswordsException_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User {Password = password });
            _cryptServiceMock.Setup(s => s.Decrypt(It.IsAny<string>())).Returns(password+1);

            var result = _userService.LoginUser(userName, password);

            _cryptServiceMock.Verify();
            Assert.AreEqual(result, Errors.Authentification);
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_UserIsNotExistsException_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns((User)null);

            var result = _userService.LoginUser(userName, password);

            Assert.AreEqual(result, Errors.Authentification);
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_Successful_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User { Password = password });
            _cryptServiceMock.Setup(s => s.Decrypt(It.IsAny<string>())).Returns(password);

            var result = _userService.LoginUser(userName, password);

            _repository.Verify();
            Assert.AreEqual(result, Errors.Success);

        }
    }
}
