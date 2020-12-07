using System.Windows;
using System.Windows.Controls;
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
            ((SignUpViewModel) this.DataContext).Password1 = ((PasswordBox) sender).Password;
        }

        private void RepeatPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)this.DataContext).Password2 = ((PasswordBox)sender).Password;
        }
    }
}
