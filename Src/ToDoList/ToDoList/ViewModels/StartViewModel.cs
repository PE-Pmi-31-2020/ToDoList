using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class StartViewModel
    {
        public RelayCommand SignInCommand { get; private set; }

        public RelayCommand SignUpCommand { get; private set; }

        public StartViewModel()
        {
            SignInCommand = new RelayCommand(ShowSignIn);
            SignUpCommand = new RelayCommand(ShowSignUp);
        }

        private void ShowSignIn()
        {
            var newWindow = new SignInWindow();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }

        private void ShowSignUp()
        {
            var newWindow = new SignUpWindow();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}