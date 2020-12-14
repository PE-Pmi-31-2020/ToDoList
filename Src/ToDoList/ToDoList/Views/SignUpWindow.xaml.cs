using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Notifications.Wpf;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml.
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            this.InitializeComponent();
            this.DataContext = new SignUpViewModel();
        }

        private void CreatePasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel) this.DataContext).Password = ((PasswordBox) sender).Password;
        }

        private void RepeatPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)this.DataContext).RepeatedPassword = ((PasswordBox)sender).Password;
        }

        private void OnWindowClose(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Cast<Window>().All(x => (x is NotificationsOverlayWindow)))
            {
                Environment.Exit(Environment.ExitCode);
            }
        }
    }
}
