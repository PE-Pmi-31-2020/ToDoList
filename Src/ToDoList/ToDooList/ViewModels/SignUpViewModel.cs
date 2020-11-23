using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows;
using ToDoList.Logic;
namespace ToDoList.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public SignUpViewModel()
        {
            userService = new UserService();
            SubmitCommand = new RelayCommand<Window>(RegisterUser);
            
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                }
            }
        }

        private string password1;
        public string Password1
        {
            get { return password1; }
            set
            {
                if(!string.Equals(password1, value))
                {
                    password1 = value;
                }
            }
        }

        private string password2;
        public string Password2
        {
            get { return password2; }
            set
            {
                if (!string.Equals(password2, value))
                {
                    password2 = value;
                }
            }
        }

        private void RegisterUser(Window window)
        {
            try
            {
                userService.CreateUser(UserName, Password1, Password2);
                if (window != null)
                {
                    window.Close();
                }
                
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if(e.Message == "UserExistsError")
                {
                    Console.WriteLine("User with username exists");
                }
                else if(e.Message == "PasswordsError")
                {
                    Console.WriteLine("Passwords do not match");
                }
            }
        }
    }
}
