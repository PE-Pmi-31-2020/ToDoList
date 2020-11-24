using System.Windows;
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
    }
}
