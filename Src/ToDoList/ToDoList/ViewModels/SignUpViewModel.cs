using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using ToDoList.BLL.Services;

namespace ToDoList.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        //private INotificationService _notificationService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public SignUpViewModel()
        {
            userService = new UserService();
            //_notificationService = new NotificationService();
            SubmitCommand = new RelayCommand<Window>(RegisterUser);
            
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this._userName, value))
                {
                    this._userName = value;
                }
            }
        }

        private string _password1;
        public string Password1
        {
            get => _password1;
            set
            {
                if(!string.Equals(_password1, value))
                {
                    _password1 = value;
                }
            }
        }

        private string _password2;
        public string Password2
        {
            get => _password2;
            set
            {
                if (!string.Equals(_password2, value))
                {
                    _password2 = value;
                }
            }
        }

        private void RegisterUser(Window window)
        {
            try
            {
                userService.CreateUser(UserName, Password1, Password2);
                //_notificationService.RunNotificationKernel();
                window?.Close();

            }catch (Exception e)
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
