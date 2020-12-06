using System.Windows;
using ToDoList.DAL.Entities;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}
