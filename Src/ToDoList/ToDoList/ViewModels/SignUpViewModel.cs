using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.BLL.Services;

namespace ToDoList.ViewModels
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userService;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Window> SubmitCommand { get; private set; }

        public SignUpViewModel()
        {
            this.userService = new UserService();
            this.SubmitCommand = new RelayCommand<Window>(this.RegisterUser);
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

        private string password1;

        public string Password1
        {
            get => this.password1;
            set
            {
                if (!string.Equals(this.password1, value))
                {
                    this.password1 = value;
                }
            }
        }

        private string password2;

        public string Password2
        {
            get => this.password2;
            set
            {
                if (!string.Equals(this.password2, value))
                {
                    this.password2 = value;
                }
            }
        }

        private void RegisterUser(Window window)
        {
            try
            {
                this.userService.CreateUser(this.UserName, this.Password1, this.Password2);
                window?.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                switch (e.Message)
                {
                    case "UserExistsError":
                        Debug.WriteLine("User with username exists");
                        break;
                    case "PasswordsError":
                        Debug.WriteLine("Passwords do not match");
                        break;
                }
            }
        }
    }
}
