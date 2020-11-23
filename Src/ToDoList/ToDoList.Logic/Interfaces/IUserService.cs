using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Database.Entities;

namespace ToDoList.Logic.Interfaces
{
    public interface IUserService
    {
        void CreateUser(String userName, String password1, String password2);
        User LoginUser(string login, string password);
    }
}
