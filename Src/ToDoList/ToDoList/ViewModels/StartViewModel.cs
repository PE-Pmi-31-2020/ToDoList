using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class StartViewModel
    {

        public RelayCommand<Window> SignInCommand { get; private set; }

        public RelayCommand<Window> SignUpCommand { get; private set; }

        public StartViewModel()
        {
            this.SignInCommand = new RelayCommand<Window>(this.ShowSignIn);
            this.SignUpCommand = new RelayCommand<Window>(this.ShowSignUp);
        }

        private void ShowSignIn(Window window)
        {
            var newWindow = new SignInWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }

        private void ShowSignUp(Window window)
        {
            var newWindow = new SignUpWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }
    }
}