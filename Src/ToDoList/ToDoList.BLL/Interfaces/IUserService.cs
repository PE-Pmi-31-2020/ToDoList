using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IUserService
    {
        Errors CreateUser(string userName, string password1, string password2);
        Errors LoginUser(string login, string password);
    }
}