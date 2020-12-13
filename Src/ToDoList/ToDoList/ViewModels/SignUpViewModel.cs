using System;
using System.ComponentModel;
using System.Diagnostics;
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
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public RelayCommand SignUpCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public SignUpViewModel()
        {
            this.userService = new UserService();
            this.SignUpCommand = new RelayCommand(this.SignUp);
            this.CancelCommand = new RelayCommand(this.Cancel);
            notificationService = new NotificationService();
        }

        private string fullName;

        public string FullName
        {
            get => this.fullName;
            set
            {
                if (!string.Equals(this.fullName, value))
                {
                    this.fullName = value;
                }
            }
        }

        private string userName;

        public string UserName
        {
            get => this.userName;
            set
            {
                if (!string.Equals(this.userName, value))
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
                if (!string.Equals(this.password, value))
                {
                    this.password = value;
                }
            }
        }

        private string repeatedPassword;

        public string RepeatedPassword
        {
            get => this.repeatedPassword;
            set
            {
                if (!string.Equals(this.repeatedPassword, value))
                {
                    this.repeatedPassword = value;
                }
            }
        }

        private void SignUp()
        {
            var res = userService.CreateUser(this.FullName, this.UserName, this.Password, this.RepeatedPassword);
            switch (res)
            {
                case Errors.Authentification:
                    return;
                case Errors.Password:
                    notificationService.ShowNotification("Password mismatch", NotificationType.Error, "Error");
                    return;
                case Errors.UserExists:
                    notificationService.ShowNotification("User is already exists", NotificationType.Error, "Error");
                    return;
                case Errors.Success:
                    notificationService.ShowNotification("User created", NotificationType.Success, "Success");
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
