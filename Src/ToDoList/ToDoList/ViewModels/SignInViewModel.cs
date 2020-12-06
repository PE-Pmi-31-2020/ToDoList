
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class SignInViewModel: INotifyPropertyChanged
    {

        private readonly UserService userService;

        public event PropertyChangedEventHandler PropertyChanged;

        private string login;

        public RelayCommand<object> SubmitCommand { get; private set; }

        public SignInViewModel()
        {
            this.SubmitCommand = new RelayCommand<object>(this.Submit);
            this.userService = new UserService();
        }

        public SecureString SecurePassword { private get; set; }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.SecurePassword = ((PasswordBox)sender).SecurePassword;
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

        public void Submit(object parameter)
        {
            PasswordBox PasswordObj = parameter as PasswordBox;
            string password = PasswordObj.Password;
            var user = userService.LoginUser(this.login, password);
            var newWindow = new MainWindow();
            
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}
