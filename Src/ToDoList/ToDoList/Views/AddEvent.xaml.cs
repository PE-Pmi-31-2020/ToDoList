using System.Windows;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml.
    /// </summary>
    public partial class AddEvent : Window
    {
        public AddEvent()
        {
            this.InitializeComponent();
            this.DataContext = new AddEventViewModel(this);
        }
    }
}
