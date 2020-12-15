using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly ICryptService _cryptService;

        public UserService()
        {
            _database = new EfUnitOfWork();
            _cryptService = new CryptService();
        }

        public UserService(IUnitOfWork repository, ICryptService cryptService)
        {
            _database = repository;
            this._cryptService = cryptService;
        }

        public Errors CreateUser(string fullName, string userName, string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                return Errors.Password;
            }
            var existingUser = _database.Users.Get(userName);
            if (existingUser != null)
            {
                return Errors.UserExists;
            }

            var user = new User { Password = _cryptService.Encrypt(password), UserName = userName, FullName = fullName };
            _database.Users.Create(user);
            _database.Save();
            AppConfig.UserId = user.Id;

            return Errors.Success;
        }


        public Errors LoginUser(string userName, string password)
        {
            var user = _database.Users.Get(userName);
            if (user == null)
            {
                return Errors.Authentification;
            }
            var decryptedPassword = _cryptService.Decrypt(user.Password);
            if (!decryptedPassword.Equals(password))
            {
                return Errors.Authentification;
            }
            AppConfig.UserId = user.Id;
            return Errors.Success;
        }

        public string GetUserFullNameById(int? id)
        {
            return id == null ? string.Empty : ((UserRepository)_database.Users).Get((int)id).FullName;
        }

        public string GetUserNameById(int? id)
        {
            return id == null ? string.Empty : ((UserRepository)_database.Users).Get((int)id).UserName;
        }
    }
}
