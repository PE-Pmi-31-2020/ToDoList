using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class SignUpViewModel
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly LoggerService _loggerService;


        public RelayCommand SignUpCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public SignUpViewModel()
        {
            _userService = new UserService();
            SignUpCommand = new RelayCommand(SignUp);
            CancelCommand = new RelayCommand(Cancel);
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();
        }

        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set
            {
                if (!string.Equals(_fullName, value))
                {
                    _fullName = value;
                }
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (!string.Equals(_userName, value))
                {
                    _userName = value;
                }
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (!string.Equals(_password, value))
                {
                    _password = value;
                }
            }
        }

        private string _repeatedPassword;

        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set
            {
                if (!string.Equals(_repeatedPassword, value))
                {
                    _repeatedPassword = value;
                }
            }
        }

        private void SignUp()
        {
            var res = _userService.CreateUser(FullName, UserName, Password, RepeatedPassword);
            switch (res)
            {
                case Errors.Authentification:
                    return;
                case Errors.Password:
                    _notificationService.ShowNotification("Password mismatch", NotificationType.Error, "Error");
                    _loggerService.LogError("Password mismatch");
                    return;
                case Errors.UserExists:
                    _notificationService.ShowNotification("User already exists", NotificationType.Error, "Error");
                    _loggerService.LogError($"User {_userName} already exists");
                    return;
                case Errors.Success:
                    _notificationService.ShowNotification("User created", NotificationType.Success, "Success");
                    _loggerService.LogInfo($"{UserName} signed up");
                    break;
                default:
                    return;
            }

            var newWindow = new MainWindow();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }

        private void Cancel()
        {
            var newWindow = new StartWindow();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}
