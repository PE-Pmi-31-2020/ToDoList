using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml.
    /// </summary>
    public partial class AddEvent
    {
        public AddEvent()
        {
            InitializeComponent();
            DataContext = new AddEventViewModel(this);
        }
    }
}
