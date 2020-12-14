using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml.
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            this.InitializeComponent();
            this.DataContext = new SignInViewModel();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignInViewModel) this.DataContext).Password = ((PasswordBox) sender).Password;
        }

        private void OnWindowClose(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Count <= 2)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }

    }
}
