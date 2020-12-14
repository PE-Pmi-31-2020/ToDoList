using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IUserService
    {
        Errors CreateUser(string fullName, string userName, string password, string repeatedPassword);
        Errors LoginUser(string login, string password);
        string GetUserFullNameById(int? id);
        string GetUserNameById(int? id);
    }
}