using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        private readonly IUserService userService;
        private readonly INotificationService notificationService;
        private readonly LoggerService loggerService;

        public RelayCommand SignInCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public SignInViewModel()
        {
            this.SignInCommand = new RelayCommand(this.SignIn);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.userService = new UserService();
            notificationService = new NotificationService();
            loggerService = new LoggerService();
        }

        private string userName;

        public string UserName
        {
            get => this.userName;
            set
            {
                if (!value.Equals(this.userName))
                {
                    this.userName = value;
                }
            }
        }

        private string password;

        public string Password
        {
            get => this.password;
            set
            {
                if (!value.Equals(this.password))
                {
                    this.password = value;
                }
            }
        }

        private void SignIn()
        {
            var res = this.userService.LoginUser(this.UserName, this.Password);
            switch (res)
            {
                case Errors.Authentification:
                    notificationService.ShowNotification("Incorrect password or login", NotificationType.Error, "Error");
                    loggerService.LogError($"Incorrect password or login was entered");
                    return;
                case Errors.Password:
                    return;
                case Errors.UserExists:
                    return;
                case Errors.Success:
                    notificationService.ShowNotification("You are logged in", NotificationType.Success, "Success");
                    loggerService.LogInfo($"{this.UserName} logged in");
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
