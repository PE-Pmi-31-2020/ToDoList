using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Moq;
using NUnit.Framework;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests.Services
{
    public class UserServiceTests
    {
        private UserService _userService;
        private static Mock<IUnitOfWork> _repository;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUnitOfWork>();
            _repository.Setup(rep => rep.Save());
            _userService = new UserService(_repository.Object);
        }

        [TestCase("user1","pas1","pas2")]
        [TestCase("user2", "pas1", "pas2")]
        [TestCase("user3", "pas1", "pas2")]
        public void CreateUser_DifferentPasswordsException_Test(string userName, string password, string repeatedPassword)
        {
            Exception exception = Assert.Throws(typeof(Exception),
               () => _userService.CreateUser(userName, password, repeatedPassword));

            Assert.AreEqual("PasswordError", exception.Message);
        }

        [TestCase("user1", "pas1", "pas1")]
        [TestCase("user2", "pas1", "pas1")]
        [TestCase("user3", "pas1", "pas1")]
        public void CreateUser_UserIsExistsException_Test(string userName, string password, string repeatedPassword)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User());

            Exception exception = Assert.Throws(typeof(Exception),
                () => _userService.CreateUser(userName, password, repeatedPassword));

            Assert.AreEqual("UserExistsError", exception.Message);
        }

        [TestCase("user1", "pas1", "pas1")]
        [TestCase("user2", "pas1", "pas1")]
        [TestCase("user3", "pas1", "pas1")]
        public void CreateUser_Successful_Test(string userName, string password, string repeatedPassword)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns((User)null);
            _repository.Setup(rep => rep.Users.Create(It.IsAny<User>()));

            _userService.CreateUser(userName, password, repeatedPassword);

            _repository.Verify();
            
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_DifferentPasswordsException_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User(){Password = Encrypt(password+userName) });
            
            Exception exception = Assert.Throws(typeof(Exception),
                () => _userService.LoginUser(userName, password));

            Assert.AreEqual("IncorrectPasswordError", exception.Message);
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_UserIsNotExistsException_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns((User)null);

            Exception exception = Assert.Throws(typeof(Exception),
                () => _userService.LoginUser(userName, password));

            Assert.AreEqual("AuthError", exception.Message);
        }

        [TestCase("user1", "pas1")]
        [TestCase("user2", "pas1")]
        [TestCase("user3", "pas1")]
        public void LoginUser_Successful_Test(string userName, string password)
        {
            _repository.Setup(rep => rep.Users.Get(It.IsAny<string>())).Returns(new User() { Password = Encrypt(password) });

            var result = _userService.LoginUser(userName, password);

            _repository.Verify();
            Assert.IsInstanceOf<User>(result);

        }
        private static string Encrypt(string clearText)
        {
            string EncryptionKey = "dsadasdsadas43";
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null) return clearText;

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
