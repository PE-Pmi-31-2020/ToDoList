using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ToDoList.Views;

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
        SignInWindow newWindow = new SignInWindow();
        Application.Current.MainWindow = newWindow;
        newWindow.Show();
        if (window != null)
        {
            window.Close();
        }

    }

    private void ShowSignUp(Window window)
    {
        SignUpWindow newWindow = new SignUpWindow();
        Application.Current.MainWindow = newWindow;
        newWindow.Show();
        if (window != null)
        {
            window.Close();
        }
    }
}