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
    }
}
