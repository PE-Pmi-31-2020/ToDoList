using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    class StartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SignInCommand { get; private set; }

        public RelayCommand<Window> SignUpCommand { get; private set; }

        public StartViewModel()
        {
            SignInCommand = new RelayCommand<Window>(ShowSignIn);
            SignUpCommand = new RelayCommand<Window>(ShowSignUp);
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