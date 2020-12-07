using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string userName, string password1, string password2);
        User LoginUser(string login, string password);
    }
}