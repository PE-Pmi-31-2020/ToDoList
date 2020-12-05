
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;

namespace ToDoList.ViewModels
{
    internal class SignInViewModel: INotifyPropertyChanged
    {

        private readonly UserService userService;

        public event PropertyChangedEventHandler PropertyChanged;

        private string login;

        private string password;

        public RelayCommand<Window> SubmitCommand { get; private set; }

        public SignInViewModel()
        {
            this.SubmitCommand = new RelayCommand<Window>(this.Submit);
            this.userService = new UserService();
        }

        public string Login
        {
            get => this.login;
            set
            {
                if (!value.Equals(this.login))
                {
                    this.login = value;
                }
            }
        }

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

        public void Submit(object param)
        {
            try
            {
                var passwordBox = param as PasswordBox;
                var password = passwordBox.Password;
                User user = this.userService.LoginUser(this.login, this.password);
                // TODO: Open a new window after login
            }
            catch (Exception exception)
            {
                // TODO: Show error message
            }
        }
    }
}
