﻿using Moq;
using NUnit.Framework;
using System.Diagnostics;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoListTests
{
    public class CryptServiceTests
    {
        private CryptService _cryptService;
        [SetUp]
        public void Setup()
        {
            _cryptService = new CryptService();
        }

        [Test]
        public void EncryptTest()
        {
            var password = "root";
            var decrypted = _cryptService.Encrypt(password);
            Debug.WriteLine(decrypted);
            var expected = "lxLFbdJxsTZzXCqo1vl5cQ==";

            Assert.AreEqual(decrypted, expected);
        }

        [Test]
        public void DecryptTest()
        {
            var decrypted = "lxLFbdJxsTZzXCqo1vl5cQ==";
            var expected = "root";
            var encrypted = _cryptService.Decrypt(decrypted);
     
            Assert.AreEqual(encrypted, expected);
        }
    }
}