﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork database;
        private readonly ICryptService cryptService;

        public UserService()
        {
            database = new EFUnitOfWork();
            cryptService = new CryptService();
        }

        public UserService(IUnitOfWork repository, ICryptService cryptService)
        {
            database = repository;
            this.cryptService = cryptService;
        }

        public Errors CreateUser(string fullName, string userName, string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                return Errors.Password;
            }
            var existingUser = database.Users.Get(userName);
            if (existingUser != null)
            {
                return Errors.UserExists;
            }

            var user = new User { Password = cryptService.Encrypt(password), UserName = userName, FullName = fullName };
            database.Users.Create(user);
>>>>>>>>> Temporary merge branch 2
            database.Save();
            AppConfig.UserId = user.Id;

            return Errors.Success;
        }


        public Errors LoginUser(string userName, string password)
        {
            var user = database.Users.Get(userName);
            if (user == null)
            {
                return Errors.Authentification;
            }
            var decryptedPassword = cryptService.Decrypt(user.Password);
            if (!decryptedPassword.Equals(password))
            {
                return Errors.Authentification;
            }
            AppConfig.UserId = user.Id;
            return Errors.Success;
        }

        public string GetUserFullNameById(int? id)
        {
            return id == null ? string.Empty : ((UserRepository)database.Users).Get((int)id).FullName;
        }

        

    }
}
