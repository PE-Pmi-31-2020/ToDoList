using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class SignInViewModel
    {
        private readonly IUserService userService;

        private string userName;

        public RelayCommand<object> SubmitCommand { get; private set; }

        public SignInViewModel()
        {
            this.SubmitCommand = new RelayCommand<object>(this.Submit);
            this.userService = new UserService();
        }

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

        public void Submit(object parameter)
        {
            PasswordBox PasswordObj = parameter as PasswordBox;
            string password = PasswordObj.Password;
            var user = userService.LoginUser(this.userName, password);
            var newWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}
