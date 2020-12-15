using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class SignInViewModel
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly LoggerService _loggerService;

        public RelayCommand SignInCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
            CancelCommand = new RelayCommand(Cancel);
            _userService = new UserService();
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (!value.Equals(_userName))
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
                if (!value.Equals(_password))
                {
                    _password = value;
                }
            }
        }

        private void SignIn()
        {
            var res = _userService.LoginUser(UserName, Password);
            switch (res)
            {
                case Errors.Authentification:
                    _notificationService.ShowNotification("Incorrect password or login", NotificationType.Error, "Error");
                    _loggerService.LogError("Incorrect password or login was entered");
                    return;
                case Errors.Password:
                    return;
                case Errors.UserExists:
                    return;
                case Errors.Success:
                    _notificationService.ShowNotification("You are logged in", NotificationType.Success, "Success");
                    _loggerService.LogInfo($"{UserName} logged in");
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
